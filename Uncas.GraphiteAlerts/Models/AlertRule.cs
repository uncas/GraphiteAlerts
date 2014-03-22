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
    }
}