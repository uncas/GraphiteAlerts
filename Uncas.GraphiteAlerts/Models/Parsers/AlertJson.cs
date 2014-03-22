using System.Collections.Generic;

namespace Uncas.GraphiteAlerts.Models.Parsers
{
    public class AlertJson
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public IEnumerable<AlertRule> Rules { get; set; }
    }
}