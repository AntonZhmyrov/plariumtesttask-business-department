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
        private const string NumberOfHostsParameterName = "@NumberOfHosts";
        private const string StartParameterName = "@Start";
        private const string EndParameterName = "@End";
        
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
            
            var (sqlScript, parameters) = BuildSqlQuery(procedureName, numberOfHosts, start, end);
            
            return await _dbContext.Set<HostInfo>().FromSqlRaw(sqlScript, parameters).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<IRouteInfo>> GetRoutesAsync(int numberOfHosts, DateTime? start, DateTime? end)
        {
            const string procedureName = "dbo.GetRoutes";

            var (sqlScript, parameters) = BuildSqlQuery(procedureName, numberOfHosts, start, end);
            
            return await _dbContext.Set<RouteInfo>().FromSqlRaw(sqlScript, parameters).ToListAsync();
        }

        private (string SqlScript, object[] Parameters) BuildSqlQuery(
            string procedureName, int numberOfHosts, DateTime? start, DateTime? end)
        {
            var numberOfHostsSqlParameter = new SqlParameter
            {
                ParameterName = NumberOfHostsParameterName, 
                Value = numberOfHosts,
                SqlDbType = SqlDbType.Int
            };

            var sqlScript = $"{procedureName} {NumberOfHostsParameterName}";
            var parameters = new List<SqlParameter> { numberOfHostsSqlParameter };
            
            if (start.HasValue)
            {
                var startSqlParameter = new SqlParameter
                {
                    ParameterName = StartParameterName, 
                    Value = start.Value,
                    SqlDbType = SqlDbType.DateTime2
                };

                sqlScript += $", {StartParameterName}";
                parameters.Add(startSqlParameter);
            }

            if (end.HasValue)
            {
                var endSqlParameter = new SqlParameter
                {
                    ParameterName = EndParameterName, 
                    Value = end,
                    SqlDbType = SqlDbType.DateTime2
                };
                
                sqlScript += $", {EndParameterName}";
                parameters.Add(endSqlParameter);
            }

            var arrayOfParameters = parameters.Cast<object>().ToArray();

            return (sqlScript, arrayOfParameters);
        }
    }
}