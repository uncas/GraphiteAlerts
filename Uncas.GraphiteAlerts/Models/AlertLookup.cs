using System;
using System.Collections.Generic;
using System.Net.Http;
using Uncas.GraphiteAlerts.Models.Parsers;

namespace Uncas.GraphiteAlerts.Models
{
    public class AlertLookup : IAlertLookup
    {
        public IEnumerable<DataPoint> Lookup(string server, string target)
        {
            var requestUri =
                new Uri(string.Format("{0}/render?target={1}&format=json", server, target));
            HttpResponseMessage response = new HttpClient().GetAsync(requestUri).Result;
            dynamic json = response.Content.ReadAsAsync<dynamic>().Result;
            return new StatsParser().Parse(json);
        }
    }
}