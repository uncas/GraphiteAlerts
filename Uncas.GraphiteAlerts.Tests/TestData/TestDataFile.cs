using System.IO;
using System.Reflection;

namespace Uncas.GraphiteAlerts.Tests.TestData
{
    internal static class TestDataFile
    {
        public static string GetGraphiteStats()
        {
            return Get("GraphiteStats.json");
        }

        public static string GetGraphiteStatsWithNull()
        {
            return Get("GraphiteStatsWithNull.json");
        }

        public static string GetAlert()
        {
            return Get("Alert.json");
        }

        private static string Get(string file)
        {
            string resourceName =
                string.Format("Uncas.GraphiteAlerts.Tests.TestData.{0}", file);
            using (Stream stream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    return string.Empty;

                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }
    }
}