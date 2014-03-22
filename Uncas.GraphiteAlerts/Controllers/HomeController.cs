using System.Collections.Generic;
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
        public ActionResult Index()
        {
            var alertEngine = new AlertEngine(new AlertLookup());
            string appdatafolder = Path.Combine(Request.PhysicalApplicationPath,
                "App_Data");
            string[] files = Directory.GetFiles(appdatafolder, "*.json");
            var result = new List<AlertViewModel>
            {
                new AlertViewModel("Stuff", AlertLevel.Ok, "", "X"),
                new AlertViewModel("Blib", AlertLevel.Error, FormatComments(3, 10), "X")
            };
            foreach (string file in files)
            {
                string json = System.IO.File.ReadAllText(file);
                Alert alert = new AlertParser().Parse(json);
                AlertResult alertResult = alertEngine.Evaluate(alert);
                result.Add(new AlertViewModel(alert.Name, alertResult.Level,
                    FormatComments(alert.Rules.First().Value, alertResult.Value),
                    string.Format("{0}/render?target={1}&width=600&height=400",
                        alert.Server, alert.Target)));
            }
            return View(result);
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