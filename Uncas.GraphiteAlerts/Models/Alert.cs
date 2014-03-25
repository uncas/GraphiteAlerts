using System.Collections.Generic;

namespace Uncas.GraphiteAlerts.Models
{
    public class Alert
    {
        public Alert(
            string server,
            string target,
            IEnumerable<AlertRule> rules,
            string name,
            string dashboardUrl)
        {
            Name = name;
            DashboardUrl = dashboardUrl;
            Rules = rules;
            Target = target;
            Server = server;
        }

        public string Server { get; private set; }
        public string Target { get; private set; }
        public IEnumerable<AlertRule> Rules { get; private set; }
        public string Name { get; private set; }
        public string DashboardUrl { get; private set; }
    }
}