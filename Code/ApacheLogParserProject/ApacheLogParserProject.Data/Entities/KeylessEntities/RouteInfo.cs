using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.Data.Entities.KeylessEntities
{
    public class RouteInfo : IRouteInfo
    {
        public string RequestRoute { get; set; }
        
        public int NumberOfRequests { get; set; }
    }
}