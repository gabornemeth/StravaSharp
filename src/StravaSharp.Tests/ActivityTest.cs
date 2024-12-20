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
using System.Numerics;
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
                Assert.That(result, Is.Not.Null.And.Not.Empty);
                Assert.That(result[0].UploadId, Is.EqualTo(new BigInteger(65459843344)));
            }
        }

        [Test]
        public async Task GetActivityZones_Parse()
        {
            var client = TestHelper.CreateFakeStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.That(activities.Count(), Is.GreaterThan(0));

            var zones = await client.Activities.GetActivityZones(activities.First().Id);
            Assert.That(zones, Is.Not.Null);
            Assert.That(zones.Count(), Is.GreaterThan(0));
            foreach (var zone in zones)
            {
                Assert.That(zone, Is.Not.Null);
                Assert.That(zone.DistributionBuckets, Is.Not.Null);
            }
        }

        [Test]
        public async Task GetSplits_Parse()
        {
            var client = TestHelper.CreateFakeStravaClient();
            var activity = await client.Activities.Get(100);
            Assert.That(activity.SplitsMetric.Length, Is.GreaterThan(0));
        }

        [Test]
        public async Task GetPhotos_Parse()
        {
            var client = TestHelper.CreateFakeStravaClient();
            var activity = await client.Activities.Get(100);
            Assert.That(activity.Photos.Count, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public async Task Update_Parse()
        {
            var client = TestHelper.CreateFakeStravaClient();
            var updatableActivity = new UpdatableActivity { SportType = SportType.MountainBikeRide };
            var activity = await client.Activities.Update(100, updatableActivity);
            Assert.That(activity.Map, Is.Not.Null);
            Assert.That(activity.SportType, Is.EqualTo(SportType.MountainBikeRide));
        }
    }
}
