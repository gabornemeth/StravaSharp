//
// ActivityTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StravaSharp.Tests.Integration
{
    [TestFixture]
    public class PolylineTest
    {
        [Test]
        public async Task DecodeMap()
        {
            var client = await TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.That(activities.Count(), Is.GreaterThan(0));
            var activity = activities.FirstOrDefault(a => a.Map?.SummaryPolyline != null);
            Assert.That(activity, Is.Not.Null);
            activity = await client.Activities.Get(activity.Id);
            Assert.That(activity, Is.Not.Null);
            var points = SharpGeo.Google.PolylineEncoder.Decode(activity.Map.SummaryPolyline);
            Assert.That(points, Is.Not.Null);
            Assert.That(points.Count, Is.GreaterThan(0));
        }
    }
}
