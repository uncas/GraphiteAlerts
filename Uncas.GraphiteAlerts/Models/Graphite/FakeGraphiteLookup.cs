using System;
using System.Collections.Generic;

namespace Uncas.GraphiteAlerts.Models.Graphite
{
    public class FakeGraphiteLookup : IGraphiteLookup
    {
        public IEnumerable<DataPoint> Lookup(string server, string target)
        {
            return new[] {new DataPoint(42, DateTime.Now)};
        }
    }
}