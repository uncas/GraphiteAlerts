namespace Uncas.GraphiteAlerts.Models.ViewModels
{
    public class AlertDto
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public string Comments { get; set; }
        public string ChartUrl { get; set; }
        public string Timestamp { get; set; }
    }
}