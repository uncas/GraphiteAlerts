using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Uncas.GraphiteAlerts.Models
{
    public class Parser
    {
        public IEnumerable<DataPoint> Parse(string jsonString)
        {
            var stats = JsonConvert.DeserializeObject<dynamic>(jsonString);
            dynamic datapoints = stats[0].datapoints;
            foreach (dynamic datapoint in datapoints)
                yield return ParseDataPoint(datapoint);
        }

        private static DataPoint ParseDataPoint(dynamic datapoint)
        {
            double value = datapoint[0].Value;
            DateTime timestamp = ConvertFromTimestamp(datapoint[1].Value);
            return new DataPoint(value, timestamp);
        }

        private static DateTime ConvertFromTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}