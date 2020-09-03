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
    }
}