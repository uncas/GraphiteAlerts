using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Uncas.GraphiteAlerts.Models.Graphite
{
    public class GraphiteLookup : IGraphiteLookup
    {
        public IEnumerable<DataPoint> Lookup(string server, string target)
        {
            var requestUri =
                new Uri(string.Format("{0}/render?target={1}&format=json", server, target));
            HttpResponseMessage response = new HttpClient().GetAsync(requestUri).Result;
            dynamic json = response.Content.ReadAsAsync<dynamic>().Result;
            return new DataPointParser().Parse(json);
        }
    }
}