using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    [TestFixture]
    public class SegmentEffortsTest : Test
    {
        [Test]
        public async Task GetSegmentEfforts_Parse()
        {
            var client = TestHelper.CreateFakeStravaClient();

            var efforts = (await client.SegmentEfforts.GetEfforts(1234)).ToArray();
            Assert.That(efforts.Length, Is.GreaterThanOrEqualTo(1));

            foreach (var effort in efforts)
            {
                Assert.That(effort.Activity, Is.Not.Null);
                Assert.That(effort.Athlete, Is.Not.Null);
                Assert.That(effort.Segment, Is.Not.Null);
            }
        }
    }
}
