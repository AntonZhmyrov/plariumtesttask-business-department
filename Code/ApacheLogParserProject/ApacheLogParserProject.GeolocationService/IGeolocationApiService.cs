using System.Threading.Tasks;

namespace ApacheLogParserProject.GeolocationService
{
    public interface IGeolocationApiService
    {
        /// <summary>
        /// Gets the geolocation response when passing a specific ip address
        /// </summary>
        Task<GeolocationResponse> GetGeolocationByIpAsync(string ipAddress);
    }
}