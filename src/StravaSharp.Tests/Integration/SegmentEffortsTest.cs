using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaSharp.Tests.Integration
{
    [TestFixture]
    public class SegmentEffortsTest : Test
    {
        [Test]
        [Ignore("Premium subscription required")]
        public async Task GetSegmentEfforts_KnownSegment()
        {
            var client = await TestHelper.CreateStravaClient();

            // panoráma: 9034772
            var efforts = (await client.SegmentEfforts.GetEfforts(9034772)).ToArray();
            Assert.That(efforts, Has.Length.GreaterThanOrEqualTo(1));

            foreach (var effort in efforts)
            {
                Assert.That(effort.Activity, Is.Not.Null);
                Assert.That(effort.Athlete, Is.Not.Null);
                Assert.That(effort.Segment, Is.Not.Null);
            }
        }

        [Test]
        [Ignore("Premium subscription required")]
        public async Task GetSegmentEfforts_KnownSegment_WithDate()
        {
            var client = await TestHelper.CreateStravaClient();

            // panoráma: 9034772
            var efforts = (await client.SegmentEfforts.GetEfforts(9034772, startDateLocal: new DateTime(2000, 1, 1), endDateLocal: DateTime.Now.AddDays(-1), perPage: 10)).ToArray();
            Assert.That(efforts, Has.Length.GreaterThanOrEqualTo(1));

            foreach (var effort in efforts)
            {
                effort.Activity.Should().NotBeNull();
                effort.Athlete.Should().NotBeNull();
                effort.Segment.Should().NotBeNull();
            }
        }

    }
}
