using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Uncas.GraphiteAlerts.Models.Graphite;
using Uncas.GraphiteAlerts.Models.Parsers;
using Uncas.GraphiteAlerts.Models.ViewModels;

namespace Uncas.GraphiteAlerts.Models
{
    public class AlertService
    {
        private static readonly Random Random = new Random();
        private readonly AlertEngine _alertEngine = new AlertEngine(new GraphiteLookup());

        public IEnumerable<AlertDto> GetAlerts(bool fake = false)
        {
            IEnumerable<AlertDto> alerts = fake ? GetFakeAlerts() : GetRealAlerts();
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

        private static IEnumerable<AlertDto> GetFakeAlerts()
        {
            return new[]
            {
                new AlertDto("Stuff", AlertLevel.Ok, "", "X", DateTime.Now,
                    "http://www.google.dk"),
                new AlertDto("Blib", AlertLevel.Critical,
                    FormatComments(3, Random.Next(10, 100)), "X",
                    DateTime.Now, "http://www.google.dk")
            };
        }

        private static string FormatComments(double limit, double actual)
        {
            return string.Format(
                "Expected to be smaller than '{0:N0}', but was '{1:N0}'.",
                limit,
                actual);
        }
    }
}