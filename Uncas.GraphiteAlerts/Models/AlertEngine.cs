using System;
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
                if (CheckRule(newestValue, rule))
                    return new AlertResult(rule.Level, newestValue, newest.Timestamp,
                        GetComment(rule, newestValue));
            }

            return new AlertResult(
                AlertLevel.Ok, newestValue, newest.Timestamp,
                string.Format("The actual value '{0:G4}' is OK.", newestValue));
        }

        private string GetComment(AlertRule rule, double newestValue)
        {
            string format = GetFormat(rule);
            return string.Format(format, rule.Value, newestValue);
        }

        private static string GetFormat(AlertRule rule)
        {
            switch (rule.Operator)
            {
                case ">":
                    return
                        "The actual value '{1:G4}' is larger than the limit at '{0:G4}'.";
                case "<":
                    return
                        "The actual value '{1:G4}' is smaller than the limit at '{0:G4}'.";
            }

            return string.Empty;
        }

        private bool CheckRule(double newestValue, AlertRule rule)
        {
            return GetRuleOperator(rule)(newestValue, rule.Value);
        }

        private Func<double, double, bool> GetRuleOperator(AlertRule rule)
        {
            switch (rule.Operator)
            {
                case ">":
                    return (actualValue, limit) => actualValue > limit;
                case "<":
                    return (actualValue, limit) => actualValue < limit;
                case "=":
                case "==":
                    return (actualValue, limit) => actualValue.Equals(limit);
                default:
                    return (actualValue, limit) => false;
            }
        }
    }
}