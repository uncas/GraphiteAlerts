using System;
using System.Collections.Generic;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Uncas.GraphiteAlerts.Models;
using Uncas.GraphiteAlerts.Models.Graphite;

namespace Uncas.GraphiteAlerts.Tests
{
    [TestFixture]
    public class AlertEngineTests : WithFixture<AlertEngine>
    {
        [Test]
        [TestCase(41, AlertLevel.Ok)]
        [TestCase(42, AlertLevel.Ok)]
        [TestCase(43, AlertLevel.Critical)]
        public void Evaluate(int value, AlertLevel expected)
        {
            Fixture.FreezeResult<IGraphiteLookup, IEnumerable<DataPoint>>(new[]
            {new DataPoint(value, A<DateTime>())});
            Fixture.Inject(
                (IEnumerable<AlertRule>) new[] {new AlertRule(">", 42, AlertLevel.Critical)});

            AlertResult alertResult = Sut.Evaluate(A<Alert>());

            Assert.That(alertResult.Level, Is.EqualTo(expected));
        }
    }
}