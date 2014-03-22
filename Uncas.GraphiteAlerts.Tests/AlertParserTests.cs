using System.Linq;
using NUnit.Framework;
using Uncas.GraphiteAlerts.Models;
using Uncas.GraphiteAlerts.Models.Parsers;
using Uncas.GraphiteAlerts.Tests.TestData;

namespace Uncas.GraphiteAlerts.Tests
{
    [TestFixture]
    public class AlertParserTests
    {
        [Test]
        public void Parse()
        {
            var sut = new AlertParser();
            Alert alert = sut.Parse(TestDataFile.GetAlert());

            Assert.That(alert.Target, Is.EqualTo("load"));
            Assert.That(alert.Rules.Count(), Is.EqualTo(1));
            Assert.That(alert.Rules.First().Level, Is.EqualTo(AlertLevel.Error));
            Assert.That(alert.Rules.First().Operator, Is.EqualTo(">"));
            Assert.That(alert.Rules.First().Value, Is.EqualTo(42));
        }
    }
}