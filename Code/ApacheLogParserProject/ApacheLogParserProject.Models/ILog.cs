using System;

namespace ApacheLogParserProject.Models
{
    public interface ILog
    {
        DateTime RequestDateTime { get; set; }

        string ClientIpAddress { get; set; }

        string ClientGeolocation { get; set; }

        string RequestRoute { get; set; }

        string RequestQueryParameters { get; set; }
        
        int ResponseCode { get; set; }

        int? ResponseSize { get; set; }
    }
}