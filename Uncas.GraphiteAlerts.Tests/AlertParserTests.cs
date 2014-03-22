using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Uncas.GraphiteAlerts.Models;
using Uncas.GraphiteAlerts.Models.Parsers;
using Uncas.GraphiteAlerts.Tests.TestData;

namespace Uncas.GraphiteAlerts.Tests
{
    [TestFixture]
    public class AlertParserTests : WithFixture<AlertParser>
    {
        [Test]
        public void Parse_Valid_Ok()
        {
            IEnumerable<Alert> alerts = Sut.Parse(TestDataFile.GetAlert());

            Assert.That(alerts, Is.Not.Empty);
            Assert.That(alerts.Count(), Is.EqualTo(2));
            Alert alert = alerts.First();
            Assert.That(alert.Target, Is.EqualTo("web.cpu"));
            Assert.That(alert.Name, Is.EqualTo("prod.web.cpu"));
            Assert.That(alert.Rules.Count(), Is.EqualTo(2));
            Assert.That(alert.Rules.First().Level, Is.EqualTo(AlertLevel.Warning));
            Assert.That(alert.Rules.First().Operator, Is.EqualTo(">"));
            Assert.That(alert.Rules.First().Value, Is.EqualTo(42));
        }
    }
}