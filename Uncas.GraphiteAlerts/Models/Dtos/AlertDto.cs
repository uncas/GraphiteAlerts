using System;

namespace Uncas.GraphiteAlerts.Models.ViewModels
{
    public class AlertDto
    {
        public AlertDto(
            string name,
            AlertLevel level,
            string comments,
            string chartUrl,
            DateTime? timestamp,
            string dashboardUrl)
        {
            DashboardUrl = dashboardUrl;
            Timestamp = GetTimestampString(timestamp);
            ChartUrl = chartUrl;
            Comments = comments;
            Level = level.ToString();
            Name = name;
        }

        public string Name { get; private set; }
        public string Level { get; private set; }
        public string Comments { get; private set; }
        public string ChartUrl { get; private set; }
        public string Timestamp { get; private set; }
        public string DashboardUrl { get; private set; }

        private string GetTimestampString(DateTime? timestamp)
        {
            if (timestamp.HasValue)
            {
                DateTime local =
                    new DateTimeOffset(timestamp.Value).ToLocalTime().DateTime;
                return local.ToString("g");
            }

            return string.Empty;
        }
    }
}