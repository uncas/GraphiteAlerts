using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Uncas.GraphiteAlerts.Models.Dtos;
using Uncas.GraphiteAlerts.Models.Graphite;
using Uncas.GraphiteAlerts.Models.Parsers;

namespace Uncas.GraphiteAlerts.Models
{
    public class AlertService
    {
        private AlertEngine _alertEngine;

        public IEnumerable<AlertDto> GetAlerts(bool fake = false)
        {
            IGraphiteLookup lookup = fake
                ? new FakeGraphiteLookup()
                : (IGraphiteLookup) new GraphiteLookup();
            _alertEngine = new AlertEngine(lookup);

            IEnumerable<AlertDto> alerts = GetRealAlerts();
            return alerts.OrderByDescending(x => x.Level).ThenBy(x => x.Name);
        }

        private IEnumerable<AlertDto> GetRealAlerts()
        {
            string folder = GetFolder();
            if (string.IsNullOrWhiteSpace(folder))
                yield break;

            var alertParser = new AlertParser();
            foreach (string file in Directory.GetFiles(folder, "*.json"))
            {
                string json = File.ReadAllText(file);
                IEnumerable<Alert> alerts = alertParser.Parse(json);
                foreach (Alert alert in alerts)
                {
                    AlertResult alertResult = _alertEngine.Evaluate(alert);
                    yield return new AlertDto(
                        alert.Name,
                        alertResult.Level,
                        alertResult.Comment,
                        string.Format("{0}/render?target={1}&width=600&height=400",
                            alert.Server, alert.Target),
                        alertResult.Timestamp,
                        alert.DashboardUrl);
                }
            }
        }

        private string GetFolder()
        {
            string alertsFolder = ConfigurationManager.AppSettings["AlertsFolder"];
            if (!string.IsNullOrWhiteSpace(alertsFolder))
                return alertsFolder;

            string physicalApplicationPath =
                HttpContext.Current.Request.PhysicalApplicationPath;
            if (string.IsNullOrWhiteSpace(physicalApplicationPath))
                return null;

            return Path.Combine(physicalApplicationPath, "App_Data");
        }
    }
}