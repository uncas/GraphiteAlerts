using System.Collections.Generic;

namespace Uncas.GraphiteAlerts.Models.Graphite
{
    public interface IGraphiteLookup
    {
        IEnumerable<DataPoint> Lookup(string server, string target);
    }
}