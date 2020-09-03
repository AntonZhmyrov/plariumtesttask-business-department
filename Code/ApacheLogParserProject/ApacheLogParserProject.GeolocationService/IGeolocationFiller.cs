using System.Collections.Generic;
using System.Threading.Tasks;
using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.GeolocationService
{
    public interface IGeolocationFiller
    {
        /// <summary>
        /// Fills provided logs with geolocation data
        /// </summary>
        Task FillLogsWithGeolocationAsync(IEnumerable<ILog> logs);
    }
}