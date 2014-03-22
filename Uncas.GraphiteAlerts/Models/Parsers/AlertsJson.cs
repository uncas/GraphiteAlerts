using System.Collections.Generic;

namespace Uncas.GraphiteAlerts.Models.Parsers
{
    public class AlertsJson
    {
        public string Server { get; set; }
        public string NamePrefix { get; set; }
        public IEnumerable<AlertJson> Alerts { get; set; }
    }
}