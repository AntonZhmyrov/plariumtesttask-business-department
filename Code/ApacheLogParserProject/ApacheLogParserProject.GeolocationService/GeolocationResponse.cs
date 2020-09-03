using Newtonsoft.Json;

namespace ApacheLogParserProject.GeolocationService
{
    /// <summary>
    /// Geolocation response
    /// </summary>
    public class GeolocationResponse
    {
        [JsonProperty("name")]
        public string CountryName { get; set; }
    }
}