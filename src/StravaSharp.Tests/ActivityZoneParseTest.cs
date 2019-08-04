using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace StravaSharp.Tests
{
    [TestFixture("activitiyZones.json")]
    public class ActivityZoneParseTest
    {
        private string _resourceName;

        public ActivityZoneParseTest(string resourceName)
        {
            _resourceName = resourceName;
        }

        [Test]
        public void Parse()
        {
            var serializer = new JsonSerializer() { ObjectCreationHandling = ObjectCreationHandling.Reuse };
            using (var stream = Resource.GetStream(_resourceName))
            {
                var reader = new JsonTextReader(new StreamReader(stream));
                var result = serializer.Deserialize<List<ActivityZone>>(reader);
                Assert.NotNull(result);
            }
        }
    }
}