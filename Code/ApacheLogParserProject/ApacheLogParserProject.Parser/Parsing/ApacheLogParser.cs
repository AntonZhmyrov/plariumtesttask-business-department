using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.Parser.Parsing
{
    public class ApacheLogParser : IApacheLogParser
    {
        private const char SpaceSymbol = ' ';

        private static readonly Regex IpAddressPattern =
            new Regex(@"^\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}", RegexOptions.Compiled);

        private static readonly Regex DateTimePattern =
            new Regex(
                @"\[\d{2}/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/\d{4}:\d{2}:\d{2}:\d{2}\s(\+|-)\d{4}\]",
                RegexOptions.Compiled);

        private static readonly Regex RequestUrlPattern =
            new Regex(@"\""(GET|HEAD|POST|PUT|DELETE|CONNECT|OPTIONS|TRACE|PATCH|T)[\w/\.\s?&=$-_\+!\*'()~]+\""",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex ResponseCodeAndResponseSizePattern =
            new Regex(@"\s\d{3}\s\d{1,19}\s", RegexOptions.Compiled);

        private static readonly Regex ResponseCodeRedirectPattern = new Regex(@"\s\d{3}\s-\s", RegexOptions.Compiled);

        private static readonly Regex FrontendRequestPattern =
            new Regex(
                @"\""(GET|HEAD|POST|PUT|DELETE|CONNECT|OPTIONS|TRACE|PATCH|T)[\w/\.\s?&=$-_\+!\*'()~]+\.(css|jpg|png|gif|ico|js)\sHTTP/\d.\d\""",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly ConcurrentBag<ILog> _logs = new ConcurrentBag<ILog>();

        /// <inheritdoc />
        public async Task<ILog[]> ParseAsync(string[] logEntries)
        {
            if (logEntries == null || !logEntries.Any())
            {
                return Enumerable.Empty<ILog>().ToArray();
            }

            // Filter out requests to get css, js or image files
            var filteredLogEntries = logEntries
                .Where(logEntry => !FrontendRequestPattern.IsMatch(logEntry))
                .ToArray();

            if (!filteredLogEntries.Any())
            {
                return Enumerable.Empty<ILog>().ToArray();
            }

            // Run log parsing on a thread pool thread, in order not to block the main thread from the app
            // Then the parallel class parallelizes work between the cores using other thread pool threads
            await Task.Run(() => Parallel.ForEach(filteredLogEntries, logEntry =>
            {
                var log = ParseInternal(logEntry);

                if (log != null)
                {
                    _logs.Add(log);
                }
            }));

            return _logs.ToArray();
        }

        private static ILog ParseInternal(string logEntry)
        {
            if (string.IsNullOrWhiteSpace(logEntry))
            {
                return null;
            }

            // Parse route data: route and query params specifically
            var (route, queryParameters) = ParseRouteData(logEntry);
            
            // Parse request datetime 
            var requestDateTime = ParseRequestDateTime(logEntry);
            
            // Parse response data: response code and response size numbers specifically
            var (responseCode, responseSize) = ParseResponseData(logEntry);

            var logModel = new LogModel
            {
                ClientIpAddress = IpAddressPattern.Match(logEntry).Value,
                RequestRoute = route,
                RequestQueryParameters = queryParameters,
                RequestDateTime = requestDateTime,
                ResponseCode = responseCode,
                ResponseSize = responseSize
            };

            return logModel;
        }

        private static (string Route, string QueryParameters) ParseRouteData(string log)
        {
            const char questionMarkSymbol = '?';
            const char ampersandSymbol = '&';
            const char newLineSymbol = '\n';

            var routeDataString = RequestUrlPattern.Match(log).Value;
            var routeDataArray = routeDataString.Split(SpaceSymbol, StringSplitOptions.RemoveEmptyEntries);
            var routeWithParams = routeDataArray[1];
            var routeWithParamsArray = routeWithParams.Split(questionMarkSymbol, StringSplitOptions.RemoveEmptyEntries);

            return (routeWithParamsArray.First(), routeWithParamsArray.Length > 1
                ? routeWithParamsArray[1].Replace(ampersandSymbol, newLineSymbol)
                : null);
        }

        private static DateTime ParseRequestDateTime(string log)
        {
            var dateTimeString = DateTimePattern.Match(log).Value.Trim('[', ']');
            var dateTimeStringValidFormat = dateTimeString.Insert(dateTimeString.Length - 2, ":");
            const string format = "dd/MMM/yyyy:HH:mm:ss zzz";

            return DateTime.ParseExact(dateTimeStringValidFormat, format, CultureInfo.InvariantCulture)
                .ToUniversalTime();
        }

        private static (int ResponseCode, int? ResponseSize) ParseResponseData(string log)
        {
            var responseDataString = ResponseCodeAndResponseSizePattern.Match(log).Value;

            if (string.IsNullOrWhiteSpace(responseDataString))
            {
                responseDataString = ResponseCodeRedirectPattern.Match(log).Value;
            }

            var responseDataArray = responseDataString.Split(SpaceSymbol, StringSplitOptions.RemoveEmptyEntries);
            var responseCode = Convert.ToInt32(responseDataArray.First());
            var isSuccess = int.TryParse(responseDataArray[1], out var responseSize);

            return (responseCode, isSuccess ? responseSize : (int?) null);
        }
    }
}