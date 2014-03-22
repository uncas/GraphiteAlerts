using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Uncas.GraphiteAlerts.Models;
using Uncas.GraphiteAlerts.Models.Parsers;
using Uncas.GraphiteAlerts.Models.ViewModels;

namespace Uncas.GraphiteAlerts.Controllers
{
    public class HomeController : Controller
    {
        private readonly AlertEngine _alertEngine = new AlertEngine(new AlertLookup());

        public ActionResult Index(bool fake = false)
        {
            IEnumerable<AlertViewModel> alerts = fake ? GetFakeAlerts() : GetAlerts();
            return View(alerts.OrderByDescending(x => x.Level).ThenBy(x => x.Name));
        }

        private IEnumerable<AlertViewModel> GetAlerts()
        {
            string folder = GetFolder();
            if (string.IsNullOrWhiteSpace(folder))
                yield break;

            var alertParser = new AlertParser();
            foreach (string file in Directory.GetFiles(folder, "*.json"))
            {
                string json = System.IO.File.ReadAllText(file);
                IEnumerable<Alert> alerts = alertParser.Parse(json);
                foreach (Alert alert in alerts)
                {
                    AlertResult alertResult = _alertEngine.Evaluate(alert);
                    yield return new AlertViewModel(
                        alert.Name,
                        alertResult.Level,
                        alertResult.Comment,
                        string.Format("{0}/render?target={1}&width=600&height=400",
                            alert.Server, alert.Target),
                        alertResult.Timestamp);
                }
            }
        }

        private string GetFolder()
        {
            string alertsFolder = ConfigurationManager.AppSettings["AlertsFolder"];
            if (!string.IsNullOrWhiteSpace(alertsFolder))
                return alertsFolder;
            if (string.IsNullOrWhiteSpace(Request.PhysicalApplicationPath))
                return null;

            return Path.Combine(Request.PhysicalApplicationPath, "App_Data");
        }

        private static IEnumerable<AlertViewModel> GetFakeAlerts()
        {
            return new[]
            {
                new AlertViewModel("Stuff", AlertLevel.Ok, "", "X", DateTime.Now),
                new AlertViewModel("Blib", AlertLevel.Critical, FormatComments(3, 10), "X",
                    DateTime.Now)
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