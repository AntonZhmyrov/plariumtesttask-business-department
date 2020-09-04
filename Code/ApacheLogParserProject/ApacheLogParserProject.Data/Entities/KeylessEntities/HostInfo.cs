using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.Data.Entities.KeylessEntities
{
    public class HostInfo : IHostInfo
    {
        public string HostIpAddress { get; set; }

        public string HostCountry { get; set; }

        public int NumberOfRequests { get; set; }
    }
}