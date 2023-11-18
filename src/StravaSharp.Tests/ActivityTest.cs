//
// ActivityTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using FluentAssertions;
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
        public async Task GetActivities()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.NotNull(activities);
            Assert.True(activities.Count() > 0);
        }

        [Test]
        public async Task GetActivitiesPage()
        {
            var client = TestHelper.CreateStravaClient();
            const int itemsPerPage = 2;
            var activities = await client.Activities.GetAthleteActivities(0, itemsPerPage);
            Assert.AreEqual(itemsPerPage, activities.Count());
        }

        [Test]
        public async Task GetActivitiesDate()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities(DateTime.Now, DateTime.Now.AddYears(-10));
            Assert.True(activities.Count() > 0);
        }

        [Test]
        public async Task GetLaps()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count() > 0);
            foreach (var activity in activities)
            {
                var laps = await client.Activities.GetLaps(activity.Id);
                laps.Should().NotBeNullOrEmpty();
            }
        }

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
        public async Task GetActivityStream()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count() > 0);

            var streams = await client.Activities.GetActivityStreams(activities.First().Id, StreamType.HeartRate, StreamType.LatLng);
            Assert.NotNull(streams);
            Assert.True(streams.Count() > 0);
            foreach (var stream in streams)
            {
                Assert.NotNull(stream);
                Assert.NotNull(stream.Data);
            }
        }

        [Test]
        public async Task GetActivityZones()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count() > 0);

            await GoOnIfPremium(client, async () =>
            {
                var zones = await client.Activities.GetActivityZones(activities.First().Id);
                zones.Should().NotBeNullOrEmpty();
                foreach (var zone in zones)
                {
                    zone.Should().NotBeNull();
                    zone.DistributionBuckets.Should().NotBeNullOrEmpty();
                }
            });
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
            var activity = await client.Activities.Update(100);
            Assert.NotNull(activity.Map);
        }
    }
}
