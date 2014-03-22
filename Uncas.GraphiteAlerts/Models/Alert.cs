using System.Collections.Generic;

namespace Uncas.GraphiteAlerts.Models
{
    public class Alert
    {
        public Alert(string server, string target, IEnumerable<AlertRule> rules)
        {
            Rules = rules;
            Target = target;
            Server = server;
        }

        public string Server { get; private set; }
        public string Target { get; private set; }
        public IEnumerable<AlertRule> Rules { get; private set; }
    }
}