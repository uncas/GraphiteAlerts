using System.Web.Mvc;
using Uncas.GraphiteAlerts.Models;

namespace Uncas.GraphiteAlerts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new[] {new AlertViewModel("Stuff")});
        }
    }
}