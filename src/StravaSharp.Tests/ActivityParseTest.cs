using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace StravaSharp.Tests
{
    [TestFixture("activity_watersport.json")]
    public class ActivityParseTest
    {
        private string _resourceName;

        public ActivityParseTest(string resourceName)
        {
            _resourceName = resourceName;
        }

        [Test]
        [Explicit("Move this to an Test.Internal project with access, and uncomment code")]
        public void Parse()
        {
            // TODO Move this to an Test.Internal project with access, and uncomment code
            //var serializer = new JsonSerializer() { ObjectCreationHandling = ObjectCreationHandling.Reuse };
            //using (var stream = Resource.GetStream(_resourceName))
            //{
            //    var reader = new JsonTextReader(new StreamReader(stream));
            //    var result = serializer.Deserialize<Activity>(reader);
            //    Assert.NotNull(result);
            //    Assert.AreEqual(361720123456, result.UploadId);
            //}
        }
    }
}
