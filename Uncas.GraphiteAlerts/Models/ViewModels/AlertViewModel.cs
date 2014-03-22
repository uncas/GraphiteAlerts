namespace Uncas.GraphiteAlerts.Models.ViewModels
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