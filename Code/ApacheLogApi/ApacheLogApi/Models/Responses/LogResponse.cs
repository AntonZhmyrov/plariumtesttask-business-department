using System;
using ApacheLogParserProject.Models;

namespace ApacheLogApi.Models.Responses
{
    public class LogResponse : ILog
    {
        public DateTime RequestDateTime { get; set; }
        
        public string ClientIpAddress { get; set; }
        
        public string ClientGeolocation { get; set; }
        
        public string RequestRoute { get; set; }
        
        public string RequestQueryParameters { get; set; }
        
        public int ResponseCode { get; set; }
        
        public int? ResponseSize { get; set; }
    }
}