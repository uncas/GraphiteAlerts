using Newtonsoft.Json;

namespace Uncas.GraphiteAlerts.Models.Parsers
{
    public class AlertParser
    {
        public Alert Parse(string jsonString)
        {
            return JsonConvert.DeserializeObject<Alert>(jsonString);
        }
    }
}