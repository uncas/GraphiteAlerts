using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Uncas.GraphiteAlerts.Models;
using Uncas.GraphiteAlerts.Models.ViewModels;

namespace Uncas.GraphiteAlerts.Controllers
{
    public class AlertController : ApiController
    {
        // GET api/alert
        public IEnumerable<AlertDto> GetAlerts()
        {
            bool fake = HttpContext.Current.Request.UrlReferrer.Query.Contains("fake");
            return new AlertService().GetAlerts(fake)
                .Select(x =>
                    new AlertDto(x.Name, x.Level, x.Comments, x.ChartUrl, x.Timestamp,
                        x.DashboardUrl));
        }
    }
}