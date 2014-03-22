using System;
using System.Collections.Generic;
using NUnit.Framework;
using Uncas.GraphiteAlerts.Models;
using Uncas.GraphiteAlerts.Tests.TestData;

namespace Uncas.GraphiteAlerts.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void Parse()
        {
            var sut = new Parser();
            IEnumerable<DataPoint> result = sut.Parse(TestDataFile.GetGraphiteStats());
            foreach (DataPoint datapoint in result)
                Console.WriteLine(datapoint);
        }
    }
}