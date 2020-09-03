using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApacheLogParserProject.Data.Extensions;
using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.Data.Repositories
{
    public class ApacheLogsRepository : IApacheLogsRepository
    {
        private readonly ApacheLogsDbContext _dbContext;

        public ApacheLogsRepository(ApacheLogsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        /// <inheritdoc/>
        public async Task<bool> CreateManyAsync(IEnumerable<ILog> logs)
        {
            var logsToSave = logs.Select(log =>
            {
                var logEntity = log.ToLogEntity();
                logEntity.PublicId = Guid.NewGuid();

                return logEntity;
            });
            
            await _dbContext.Logs.AddRangeAsync(logsToSave);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}