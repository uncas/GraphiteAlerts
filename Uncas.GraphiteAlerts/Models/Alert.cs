using System.Collections.Generic;

namespace Uncas.GraphiteAlerts.Models
{
    public class Alert
    {
        public string Target { get; set; }
        public IEnumerable<AlertRule> Rules { get; set; }
    }
}