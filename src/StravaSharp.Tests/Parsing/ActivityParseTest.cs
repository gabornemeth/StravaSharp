using NUnit.Framework;

namespace StravaSharp.Tests.Parsing;

[TestFixture]
public class ActivityParseTest : ParseTest
{
    [Test]
    public void Parse_Mountainbike()
    {
        Parse<Activity>("activity_mountainbike.json", activity =>
        {
            Assert.AreEqual(SportType.MountainBikeRide, activity.SportType);
        });
    }

    [Test]
    public void Parse_WaterSport()
    {
        Parse<Activity>("activity_watersport.json", activity =>
        {
            Assert.True(361720123456 == activity.UploadId);
#pragma warning disable CS0612 // Type or member is obsolete
            Assert.AreEqual(SportType.WaterSport, activity.SportType);
#pragma warning restore CS0612 // Type or member is obsolete
        });
    }
}
