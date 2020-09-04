namespace ApacheLogParserProject.Models
{
    public interface IHostInfo
    {
        string HostIpAddress { get; set; }

        string HostCountry { get; set; }

        int NumberOfRequests { get; set; }
    }
}