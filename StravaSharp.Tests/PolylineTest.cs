//
// ActivityTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    [TestFixture]
    public class PolylineTest
    {
        [Test]
        public async Task DecodeMap()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count > 0);
            var activity = activities[0];
            Assert.NotNull(activity);
            activity = await client.Activities.Get(activity.Id);
            Assert.NotNull(activity);
            var points = PolylineDecoder.DecodePolylinePoints(activity.Map.SummaryPolyline);
            Assert.NotNull(points);
            Assert.True(points.Count > 0);
        }
    }
}
