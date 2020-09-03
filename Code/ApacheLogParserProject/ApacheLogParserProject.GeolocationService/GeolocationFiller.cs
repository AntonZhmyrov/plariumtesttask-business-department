using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.GeolocationService
{
    public class GeolocationFiller : IGeolocationFiller
    {
        private readonly IGeolocationApiService _geolocationApiService;
        
        public GeolocationFiller()
        {
            _geolocationApiService = new GeolocationApiService();
        }
        
        /// <inheritdoc />
        public async Task FillLogsWithGeolocationAsync(IEnumerable<ILog> logs)
        {
            var ipAddressesToLogMap = logs.GroupBy(log => log.ClientIpAddress).ToArray();
            var ipAddresses = ipAddressesToLogMap.Select(group => group.Key);
            var ipAddressToCountryMap = await LoadManyGeolocationsAsync(ipAddresses);

            foreach (var mapElement in ipAddressesToLogMap)
            {
                var groupOfLogs = mapElement.ToArray();
                var success = ipAddressToCountryMap.TryGetValue(mapElement.Key, out var country);

                if (!success)
                {
                    continue;
                }

                foreach (var logFromGroup in groupOfLogs)
                {
                    logFromGroup.ClientGeolocation = country;
                }
            }
        }

        private async Task<Dictionary<string, string>> LoadManyGeolocationsAsync(IEnumerable<string> ipAddresses)
        {
            var ipAddressToCountryMap = new Dictionary<string, string>();

            foreach (var ipAddress in ipAddresses)
            {
                var geolocationResponse = await _geolocationApiService.GetGeolocationByIpAsync(ipAddress);

                if (geolocationResponse != null)
                {
                    ipAddressToCountryMap.Add(ipAddress, geolocationResponse.CountryName);
                }
            }

            return ipAddressToCountryMap;
        }
    }
}