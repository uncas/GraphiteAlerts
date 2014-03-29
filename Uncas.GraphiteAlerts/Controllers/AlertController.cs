using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Uncas.GraphiteAlerts.Models;
using Uncas.GraphiteAlerts.Models.Dtos;

namespace Uncas.GraphiteAlerts.Controllers
{
    public class AlertController : ApiController
    {
        // GET api/alert
        public IEnumerable<AlertDto> GetAlerts()
        {
            bool fake = HttpContext.Current.Request.UrlReferrer.Query.Contains("fake");
            return new AlertService().GetAlerts(fake);
        }
    }
}