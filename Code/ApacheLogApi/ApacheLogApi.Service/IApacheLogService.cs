using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApacheLogParserProject.Models;

namespace ApacheLogApi.Service
{
    public interface IApacheLogService
    {
        Task<IEnumerable<IHostInfo>> GetHostsAsync(int numberOfHosts, DateTime? start, DateTime? end);
    }
}