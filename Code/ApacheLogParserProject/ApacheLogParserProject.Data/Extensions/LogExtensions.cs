using ApacheLogParserProject.Data.Entities;
using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.Data.Extensions
{
    public static class LogExtensions
    {
        public static Log ToLogEntity(this ILog log)
        {
            return new Log
            {
                ClientGeolocation = log.ClientGeolocation,
                RequestRoute = log.RequestRoute,
                ResponseCode = log.ResponseCode,
                ResponseSize = log.ResponseSize,
                ClientIpAddress = log.ClientIpAddress,
                RequestDateTime = log.RequestDateTime,
                RequestQueryParameters = log.RequestQueryParameters
            };
        }
    }
}