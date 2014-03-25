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
                GetAlert(alerts, x));
        }

        private static Alert GetAlert(AlertsJson alerts, AlertJson alert)
        {
            return new Alert(
                alerts.Server,
                alert.Target,
                alert.Rules,
                string.Concat(alerts.NamePrefix, alert.Name),
                alert.DashboardUrl);
        }
    }
}