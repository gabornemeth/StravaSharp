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
using System.Linq;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    [TestFixture]
    public class ActivityTest : Test
    {
        [Test]
        public void ParseJson()
        {
            var serializer = new JsonSerializer() { ObjectCreationHandling = ObjectCreationHandling.Reuse };
            using (var stream = Resource.GetStream("activities.json"))
            {
                var reader = new JsonTextReader(new StreamReader(stream));
                //var result = serializer.Deserialize(reader);
                var result = serializer.Deserialize<List<ActivitySummary>>(reader);
                Assert.NotNull(result);
                Assert.IsNotEmpty(result);
                Assert.True(65459843344 == result[0].UploadId);
            }
        }

        [Test]
        public async Task GetActivityZones_Parse()
        {
            var client = TestHelper.CreateFakeStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count() > 0);

            var zones = await client.Activities.GetActivityZones(activities.First().Id);
            Assert.NotNull(zones);
            Assert.True(zones.Count() > 0);
            foreach (var zone in zones)
            {
                Assert.NotNull(zone);
                Assert.NotNull(zone.DistributionBuckets);
            }
        }

        [Test]
        public async Task GetSplits_Parse()
        {
            var client = TestHelper.CreateFakeStravaClient();
            var activity = await client.Activities.Get(100);
            Assert.True(activity.SplitsMetric.Length > 0);
        }

        [Test]
        public async Task GetPhotos_Parse()
        {
            var client = TestHelper.CreateFakeStravaClient();
            var activity = await client.Activities.Get(100);
            Assert.True(activity.Photos.Count >= 0);
        }

        [Test]
        public async Task Update_Parse()
        {
            var client = TestHelper.CreateFakeStravaClient();
            var updatableActivity = new UpdatableActivity { SportType = SportType.MountainBikeRide };
            var activity = await client.Activities.Update(100, updatableActivity);
            Assert.NotNull(activity.Map);
            Assert.AreEqual(SportType.MountainBikeRide, activity.SportType);
        }
    }
}
