using System;
using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.Data.Entities
{
    public class Log : ILog
    {
        public long Id { get; set; }

        public Guid PublicId { get; set; }

        public DateTime RequestDateTime { get; set; }

        public string ClientIpAddress { get; set; }

        public string ClientGeolocation { get; set; }

        public string RequestRoute { get; set; }

        public string RequestQueryParameters { get; set; }
        
        public int ResponseCode { get; set; }

        public int? ResponseSize { get; set; }
    }
}