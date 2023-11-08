using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Assert.GreaterOrEqual(efforts.Length, 1);

            foreach (var effort in efforts)
            {
                Assert.NotNull(effort.Activity);
                Assert.NotNull(effort.Athlete);
                Assert.NotNull(effort.Segment);
            }
        }
    }
}
