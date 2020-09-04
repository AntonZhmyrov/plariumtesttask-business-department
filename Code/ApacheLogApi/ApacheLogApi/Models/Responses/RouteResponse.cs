using ApacheLogParserProject.Models;

namespace ApacheLogApi.Models.Responses
{
    public class RouteResponse : IRouteInfo
    {
        public string RequestRoute { get; set; }
        
        public int NumberOfRequests { get; set; }
    }
}