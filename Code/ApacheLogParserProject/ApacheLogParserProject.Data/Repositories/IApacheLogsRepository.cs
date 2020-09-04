using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.Data.Repositories
{
    public interface IApacheLogsRepository
    {
        /// <summary>
        /// Create range of log entities in the database
        /// </summary>
        Task<bool> CreateManyAsync(IEnumerable<ILog> logs);

        /// <summary>
        /// Gets the clients hosts from logs
        /// </summary>
        Task<IEnumerable<IHostInfo>> GetHostsAsync(int numberOfHosts, DateTime? start, DateTime? end);

        /// <summary>
        /// Gets the routes from routes
        /// </summary>
        Task<IEnumerable<IRouteInfo>> GetRoutesAsync(int numberOfHosts, DateTime? start, DateTime? end);

        /// <summary>
        /// Get logs
        /// </summary>
        Task<IEnumerable<ILog>> GetLogsAsync(int offset, int limit, DateTime? start, DateTime? end);
    }
}