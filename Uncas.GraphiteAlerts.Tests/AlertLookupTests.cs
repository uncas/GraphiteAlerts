using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using Uncas.GraphiteAlerts.Models.Graphite;

namespace Uncas.GraphiteAlerts.Tests
{
    [TestFixture]
    [Explicit]
    public class AlertLookupTests : WithFixture<GraphiteLookup>
    {
        [Test]
        public void Lookup_Graphite_Datapoints()
        {
            string server = ConfigurationManager.AppSettings["GraphiteTestServer"];
            string target = ConfigurationManager.AppSettings["GraphiteTestTarget"];
            IEnumerable<DataPoint> result = Sut.Lookup(server, target);
        }
    }
}