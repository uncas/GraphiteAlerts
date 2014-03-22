namespace Uncas.GraphiteAlerts.Models.ViewModels
{
    public class AlertViewModel
    {
        public AlertViewModel(string name, AlertLevel level, string comments, string chartUrl)
        {
            ChartUrl = chartUrl;
            Comments = comments;
            Level = level;
            Name = name;
        }

        public string Name { get; private set; }
        public AlertLevel Level { get; private set; }
        public string Comments { get; private set; }
        public string ChartUrl { get; private set; }
    }
}