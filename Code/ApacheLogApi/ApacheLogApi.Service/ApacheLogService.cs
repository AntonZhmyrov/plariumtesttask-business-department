using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApacheLogParserProject.Data.Repositories;
using ApacheLogParserProject.Models;

namespace ApacheLogApi.Service
{
    public class ApacheLogService : IApacheLogService
    {
        private readonly IApacheLogsRepository _apacheLogsRepository;
        
        public ApacheLogService(IApacheLogsRepository apacheLogsRepository)
        {
            _apacheLogsRepository = apacheLogsRepository;
        }

        public async Task<IEnumerable<IHostInfo>> GetHostsAsync(int numberOfHosts, DateTime? start, DateTime? end)
            => await _apacheLogsRepository.GetHostsAsync(numberOfHosts, start, end);
    }
}