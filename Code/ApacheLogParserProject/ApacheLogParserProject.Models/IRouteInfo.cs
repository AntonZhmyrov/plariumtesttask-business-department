namespace ApacheLogParserProject.Models
{
    public interface IRouteInfo
    {
        string RequestRoute { get; set; }

        int NumberOfRequests { get; set; }
    }
}