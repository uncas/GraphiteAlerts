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

        public AlertLevel Evaluate(Alert alert)
        {
            IEnumerable<DataPoint> dataPoints = _alertLookup.Lookup(alert.Server, alert.Target);
            DataPoint newest =
                dataPoints.OrderByDescending(x => x.Timestamp).FirstOrDefault();
            foreach (AlertRule rule in alert.Rules)
                if (CheckRule(newest, rule))
                    return rule.Level;
            return AlertLevel.Ok;
        }

        private bool CheckRule(DataPoint newest, AlertRule rule)
        {
            return GetRuleOperator(rule)(newest.Value, rule.Value);
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