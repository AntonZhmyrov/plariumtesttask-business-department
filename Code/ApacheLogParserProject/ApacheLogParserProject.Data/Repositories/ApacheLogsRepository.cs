using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ApacheLogParserProject.Data.Entities.KeylessEntities;
using ApacheLogParserProject.Data.Extensions;
using ApacheLogParserProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        /// <inheritdoc/>
        public async Task<IEnumerable<IHostInfo>> GetHostsAsync(int numberOfHosts, DateTime? start, DateTime? end)
        {
            const string procedureName = "dbo.GetHosts";
            const string numberOfHostsParameterName = "@NumberOfHosts";
            const string startParameterName = "@Start";
            const string endParameterName = "@End";

            var numberOfHostsSqlParameter = new SqlParameter
            {
                ParameterName = numberOfHostsParameterName, 
                Value = numberOfHosts,
                SqlDbType = SqlDbType.Int
            };

            var sqlScript = $"{procedureName} {numberOfHostsParameterName}";
            var parameters = new List<SqlParameter> { numberOfHostsSqlParameter };

            if (start.HasValue)
            {
                var startSqlParameter = new SqlParameter
                {
                    ParameterName = startParameterName, 
                    Value = start.Value,
                    SqlDbType = SqlDbType.DateTime2
                };

                sqlScript += $", {startParameterName}";
                parameters.Add(startSqlParameter);
            }

            if (end.HasValue)
            {
                var endSqlParameter = new SqlParameter
                {
                    ParameterName = endParameterName, 
                    Value = end,
                    SqlDbType = SqlDbType.DateTime2
                };
                
                sqlScript += $", {endParameterName}";
                parameters.Add(endSqlParameter);
            }

            var arrayOfParameters = parameters.Cast<object>().ToArray();
            
            return await _dbContext.Set<HostInfo>().FromSqlRaw(sqlScript, arrayOfParameters).ToListAsync();
        }
    }
}