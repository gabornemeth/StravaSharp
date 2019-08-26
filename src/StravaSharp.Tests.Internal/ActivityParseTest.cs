using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace StravaSharp.Tests
{
    [TestFixture("StravaSharp.Tests.Internal.Files.activity_watersport.json")]
    public class ActivityParseTest
    {
        private readonly string _resourceName;

        public ActivityParseTest(string resourceName)
        {
            _resourceName = resourceName;
        }

        [Test]
        public void Parse()
        {
            var serializer = new JsonSerializer() { ObjectCreationHandling = ObjectCreationHandling.Reuse };
            using (var stream = Assembly.GetAssembly(GetType()).GetManifestResourceStream(_resourceName))
            {
                var reader = new JsonTextReader(new StreamReader(stream));
                var result = serializer.Deserialize<Activity>(reader);
                Assert.NotNull(result);
                Assert.AreEqual(361720123456, result.UploadId);
            }
        }

        [Test]
        public void ParseJson()
        {
            var serializer = new JsonSerializer() { ObjectCreationHandling = ObjectCreationHandling.Reuse };
            using (var stream = Assembly.GetAssembly(GetType()).GetManifestResourceStream("StravaSharp.Tests.Internal.Files.activities.json"))
            {
                var reader = new JsonTextReader(new StreamReader(stream));
                //var result = serializer.Deserialize(reader);
                var result = serializer.Deserialize<List<ActivitySummary>>(reader);
                Assert.NotNull(result);
                Assert.IsNotEmpty(result);
                Assert.AreEqual(65459843344, result[0].UploadId);
            }
        }

    }
}
