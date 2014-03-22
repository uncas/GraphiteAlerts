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
        public void Parse()
        {
            Alert alert = Sut.Parse(TestDataFile.GetAlert());

            Assert.That(alert.Target, Is.EqualTo("load"));
            Assert.That(alert.Rules.Count(), Is.EqualTo(1));
            Assert.That(alert.Rules.First().Level, Is.EqualTo(AlertLevel.Error));
            Assert.That(alert.Rules.First().Operator, Is.EqualTo(">"));
            Assert.That(alert.Rules.First().Value, Is.EqualTo(42));
        }
    }
}