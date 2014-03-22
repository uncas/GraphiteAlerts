namespace Uncas.GraphiteAlerts.Models
{
    public class AlertViewModel
    {
        public AlertViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}