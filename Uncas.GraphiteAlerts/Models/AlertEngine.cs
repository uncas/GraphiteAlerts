using System;
using System.Collections.Generic;
using System.Linq;

namespace Uncas.GraphiteAlerts.Models
{
    public class AlertEngine
    {
        private readonly IAlertLookup _alertLookup;

        public AlertEngine(IAlertLookup alertLookup)
        {
            _alertLookup = alertLookup;
        }

        public AlertResult Evaluate(Alert alert)
        {
            IEnumerable<DataPoint> dataPoints =
                _alertLookup.Lookup(alert.Server, alert.Target);
            DataPoint newest =
                dataPoints.Where(y => y.Value.HasValue)
                    .OrderByDescending(x => x.Timestamp)
                    .FirstOrDefault();
            if (newest == null || !newest.Value.HasValue)
                return new AlertResult(AlertLevel.Warn, 0d, null);

            double newestValue = newest.Value.Value;
            foreach (AlertRule rule in alert.Rules)
            {
                if (CheckRule(newestValue, rule))
                    return new AlertResult(rule.Level, newestValue, newest.Timestamp);
            }

            return new AlertResult(AlertLevel.Ok, newestValue, newest.Timestamp);
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