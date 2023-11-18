using FluentAssertions;
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

        [Test]
        public async Task GetSegmentEfforts_KnownSegment()
        {
            var client = TestHelper.CreateStravaClient();

            // panoráma: 9034772
            var efforts = (await client.SegmentEfforts.GetEfforts(9034772)).ToArray();
            Assert.GreaterOrEqual(efforts.Length, 1);

            foreach (var effort in efforts)
            {
                Assert.NotNull(effort.Activity);
                Assert.NotNull(effort.Athlete);
                Assert.NotNull(effort.Segment);
            }
        }

        [Test]
        public async Task GetSegmentEfforts_KnownSegment_WithDate()
        {
            var client = TestHelper.CreateStravaClient();

            // panoráma: 9034772
            var efforts = (await client.SegmentEfforts.GetEfforts(9034772, startDateLocal: new DateTime(2000, 1, 1), endDateLocal: DateTime.Now.AddDays(-1), perPage: 10)).ToArray();
            Assert.GreaterOrEqual(efforts.Length, 1);

            foreach (var effort in efforts)
            {
                effort.Activity.Should().NotBeNull();
                effort.Athlete.Should().NotBeNull();
                effort.Segment.Should().NotBeNull();
            }
        }

    }
}
