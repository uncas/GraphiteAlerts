using System;
using System.Collections.Generic;
using NUnit.Framework;
using Uncas.GraphiteAlerts.Models;
using Uncas.GraphiteAlerts.Models.Parsers;
using Uncas.GraphiteAlerts.Tests.TestData;

namespace Uncas.GraphiteAlerts.Tests
{
    [TestFixture]
    public class StatsParserTests : WithFixture<StatsParser>
    {
        [Test]
        public void Parse()
        {
            IEnumerable<DataPoint> result = Sut.Parse(TestDataFile.GetGraphiteStats());

            foreach (DataPoint datapoint in result)
                Console.WriteLine(datapoint);
        }

        [Test]
        public void Parse_WithNull_Ok()
        {
            IEnumerable<DataPoint> result =
                Sut.Parse(TestDataFile.GetGraphiteStatsWithNull());

            foreach (DataPoint datapoint in result)
                Console.WriteLine(datapoint);
        }
    }
}