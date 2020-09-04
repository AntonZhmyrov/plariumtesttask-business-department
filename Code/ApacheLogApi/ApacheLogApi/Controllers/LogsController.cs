using System;
using System.Linq;
using System.Threading.Tasks;
using ApacheLogApi.Models.Responses;
using ApacheLogParserProject.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApacheLogApi.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        private readonly IApacheLogsRepository _apacheLogsRepository;
        
        public LogsController(IApacheLogsRepository apacheLogsRepository)
        {
            _apacheLogsRepository = apacheLogsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogsAsync(
            [FromQuery] int offset, 
            [FromQuery] int limit = 10, 
            [FromQuery] DateTime? start = null, 
            [FromQuery] DateTime? end = null)
        {
            var logs = 
                await _apacheLogsRepository.GetLogsAsync(offset, limit, start, end);

            return Ok(logs.Select(log => new LogResponse
            {
                ClientGeolocation = log.ClientGeolocation,
                RequestRoute = log.RequestRoute,
                ResponseCode = log.ResponseCode,
                ResponseSize = log.ResponseSize,
                ClientIpAddress = log.ClientIpAddress,
                RequestDateTime = log.RequestDateTime,
                RequestQueryParameters = log.RequestQueryParameters
            }));
        }
        
        [HttpGet("hosts")]
        public async Task<IActionResult> GetHostsAsync(
            [FromQuery] int numberOfHosts = 10,
            [FromQuery] DateTime? start = null, 
            [FromQuery] DateTime? end = null)
        {
            var hosts = await _apacheLogsRepository.GetHostsAsync(numberOfHosts, start, end);

            return Ok(hosts.Select(
                host => new HostResponse
                {
                    HostCountry = host.HostCountry,
                    HostIpAddress = host.HostIpAddress,
                    NumberOfRequests = host.NumberOfRequests
                }));
        }

        [HttpGet("routes")]
        public async Task<IActionResult> GetRoutesAsync(
            [FromQuery] int numberOfHosts = 10,
            [FromQuery] DateTime? start = null, 
            [FromQuery] DateTime? end = null)
        {
            var routes = await _apacheLogsRepository.GetRoutesAsync(numberOfHosts, start, end);

            return Ok(routes.Select(
                route => new RouteResponse
                {
                    RequestRoute = route.RequestRoute, 
                    NumberOfRequests = route.NumberOfRequests
                }));
        }
    }
}