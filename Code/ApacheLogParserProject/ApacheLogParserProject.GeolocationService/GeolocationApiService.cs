using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApacheLogParserProject.GeolocationService
{
    /// <summary>
    /// This is the implementation which uses the free API https://ipgeolocationapi.com/
    /// </summary>
    public class GeolocationApiService : IGeolocationApiService
    {
        private const string GeolocationIpUrlApiAddress = "https://api.ipgeolocationapi.com/geolocate/";
        private static readonly HttpClient HttpClient;
        
        static GeolocationApiService()
        {
            HttpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        }

        /// <inheritdoc />
        public async Task<GeolocationResponse> GetGeolocationByIpAsync(string ipAddress)
        {
            var requestUrl = GeolocationIpUrlApiAddress + ipAddress;
            var response = await HttpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<GeolocationResponse>(content);
        }
    }
}