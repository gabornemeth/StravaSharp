using System.Collections.Generic;
using NUnit.Framework;

namespace StravaSharp.Tests.Parsing;

[TestFixture]
public class ActivityZoneParseTest : ParseTest
{
    [Test]
    public void Parse()
    {
        Parse<List<ActivityZone>>("activitiyZones.json", zones =>
        {
            Assert.That(zones, Has.Count.GreaterThan(0));
        });
    }
}