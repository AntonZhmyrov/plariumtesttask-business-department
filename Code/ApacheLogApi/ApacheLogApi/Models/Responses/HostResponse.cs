﻿using ApacheLogParserProject.Models;

namespace ApacheLogApi.Models.Responses
{
    public class HostResponse : IHostInfo
    {
        public string HostIpAddress { get; set; }

        public string HostCountry { get; set; }

        public int NumberOfRequests { get; set; }
    }
}