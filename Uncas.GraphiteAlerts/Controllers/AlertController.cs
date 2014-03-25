using System;
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
            return new AlertService().GetAlerts(fake).Select(x => new AlertDto
            {
                ChartUrl = x.ChartUrl,
                Comments = x.Comments,
                Level = x.Level.ToString(),
                Name = x.Name,
                Timestamp = GetTimestampString(x.Timestamp),
                DashboardUrl = x.DashboardUrl
            });
        }

        private string GetTimestampString(DateTime? timestamp)
        {
            if (timestamp.HasValue)
            {
                DateTime local =
                    new DateTimeOffset(timestamp.Value).ToLocalTime().DateTime;
                return local.ToString("g");
            }

            return string.Empty;
        }
    }
}