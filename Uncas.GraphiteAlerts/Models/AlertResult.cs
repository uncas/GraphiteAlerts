namespace Uncas.GraphiteAlerts.Models
{
    public class AlertResult
    {
        private readonly AlertLevel _level;
        private readonly double _value;

        public AlertResult(AlertLevel level, double value)
        {
            _level = level;
            _value = value;
        }

        public AlertLevel Level
        {
            get { return _level; }
        }

        public double Value
        {
            get { return _value; }
        }
    }
}