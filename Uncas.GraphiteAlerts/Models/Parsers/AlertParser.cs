using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Uncas.GraphiteAlerts.Models.Parsers
{
    public class AlertParser
    {
        public IEnumerable<Alert> Parse(string jsonString)
        {
            var alerts = JsonConvert.DeserializeObject<AlertsJson>(jsonString);
            return alerts.Alerts.Select(x =>
                new Alert(alerts.Server, x.Target, x.Rules,
                    string.Concat(alerts.NamePrefix, x.Name)));
        }
    }
}