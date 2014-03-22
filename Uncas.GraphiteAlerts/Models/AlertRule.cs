namespace Uncas.GraphiteAlerts.Models
{
    public class AlertRule
    {
        public string Operator { get; set; }
        public double Value { get; set; }
        public string Level { get; set; }
    }
}