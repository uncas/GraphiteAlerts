using System;
using System.Collections.Generic;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Uncas.GraphiteAlerts.Models;

namespace Uncas.GraphiteAlerts.Tests
{
    [TestFixture]
    public class AlertEngineTests : WithFixture<AlertEngine>
    {
        [Test]
        [TestCase(41, AlertLevel.Ok)]
        [TestCase(42, AlertLevel.Ok)]
        [TestCase(43, AlertLevel.Error)]
        public void Evaluate(int value, AlertLevel expected)
        {
            Fixture.FreezeResult<IAlertLookup, IEnumerable<DataPoint>>(new[]
            {new DataPoint(value, A<DateTime>())});
            Fixture.Inject(
                (IEnumerable<AlertRule>) new[] {new AlertRule(">", 42, AlertLevel.Error)});

            AlertResult alertResult = Sut.Evaluate(A<Alert>());

            Assert.That(alertResult.Level, Is.EqualTo(expected));
        }
    }
}