using System;

namespace Uncas.GraphiteAlerts.Models.Graphite
{
    public class DataPoint
    {
        public DataPoint(double? value, DateTime timestamp)
        {
            Timestamp = timestamp;
            Value = value;
        }

        public double? Value { get; private set; }
        public DateTime Timestamp { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Timestamp, Value);
        }
    }
}