using System;

namespace Uncas.GraphiteAlerts.Models
{
    public class AlertRule
    {
        public AlertRule(string @operator, double value, AlertLevel level)
        {
            Level = level;
            Value = value;
            Operator = @operator;
        }

        public string Operator { get; private set; }
        public double Value { get; private set; }
        public AlertLevel Level { get; private set; }

        public bool CheckRule(double newestValue)
        {
            return GetOperatorFunc()(newestValue, Value);
        }

        public string GetErrorComment(double newestValue)
        {
            string format = GetErrorMessageFormat();
            return string.Format(format, Value, newestValue);
        }

        private string GetErrorMessageFormat()
        {
            switch (Operator)
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

        private Func<double, double, bool> GetOperatorFunc()
        {
            switch (Operator)
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