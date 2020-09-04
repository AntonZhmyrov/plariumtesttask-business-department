namespace ApacheLogParserProject.Models
{
    public interface IHostInfo
    {
        public string HostIpAddress { get; set; }

        public string HostCountry { get; set; }

        public int NumberOfRequests { get; set; }
    }
}