using FluentAssertions;
using NUnit.Framework;
using System.Numerics;

namespace StravaSharp.Tests.Parsing;

[TestFixture]
public class ActivityParseTest : ParseTest
{
    [Test]
    public void Parse_Mountainbike()
    {
        Parse<Activity>("activity_mountainbike.json", activity =>
        {
            Assert.That(activity.SportType, Is.EqualTo(SportType.MountainBikeRide));
        });
    }

    [Test]
    public void Parse_WaterSport()
    {
        Parse<Activity>("activity_watersport.json", activity =>
        {
            activity.UploadId.Should().Be(361720123456);
#pragma warning disable CS0612 // Type or member is obsolete
            activity.SportType.Should().Be(SportType.WaterSport);
#pragma warning restore CS0612 // Type or member is obsolete
        });
    }
}
