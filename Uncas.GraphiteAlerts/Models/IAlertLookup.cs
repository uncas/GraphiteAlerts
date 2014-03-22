using System.Collections.Generic;

namespace Uncas.GraphiteAlerts.Models
{
    public interface IAlertLookup
    {
        IEnumerable<DataPoint> Lookup(string server, string target);
    }
}