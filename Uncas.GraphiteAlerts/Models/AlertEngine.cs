using System.Collections.Generic;
using System.Linq;
using Uncas.GraphiteAlerts.Models.Graphite;

namespace Uncas.GraphiteAlerts.Models
{
    public class AlertEngine
    {
        private readonly IGraphiteLookup _graphiteLookup;

        public AlertEngine(IGraphiteLookup graphiteLookup)
        {
            _graphiteLookup = graphiteLookup;
        }

        public AlertResult Evaluate(Alert alert)
        {
            IEnumerable<DataPoint> dataPoints =
                _graphiteLookup.Lookup(alert.Server, alert.Target);
            DataPoint newest =
                dataPoints.Where(y => y.Value.HasValue)
                    .OrderByDescending(x => x.Timestamp)
                    .FirstOrDefault();
            if (newest == null || !newest.Value.HasValue)
                return new AlertResult(AlertLevel.Warning, 0d, null, "No data.");

            double newestValue = newest.Value.Value;
            foreach (AlertRule rule in alert.Rules)
            {
                if (rule.CheckRule(newestValue))
                    return new AlertResult(rule.Level, newestValue, newest.Timestamp,
                        rule.GetErrorComment(newestValue));
            }

            return new AlertResult(
                AlertLevel.Ok, newestValue, newest.Timestamp,
                string.Format("The actual value '{0:G4}' is OK.", newestValue));
        }
    }
}