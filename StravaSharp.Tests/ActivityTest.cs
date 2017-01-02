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
    public class ActivityTest
    {
        [Test]
        public async Task GetActivities()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count > 0);
        }

        [Test]
        public async Task GetActivitiesOfFollowedAthletes()
        {
            var client = TestHelper.CreateStravaClient();
            var currentAthlete = await client.Athletes.GetCurrent();
            var activities = await client.Activities.GetFollowingActivities();
            Assert.True(activities.Count > 0);
            var otherActivities = activities.Where(activity => activity.Athlete.Id != currentAthlete.Id);
            Assert.True(otherActivities.Count() > 0);
        }

        [Test]
        public async Task GetActivitiesPage()
        {
            var client = TestHelper.CreateStravaClient();
            const int itemsPerPage = 2;
            var activities = await client.Activities.GetAthleteActivities(0, itemsPerPage);
            Assert.AreEqual(itemsPerPage, activities.Count);
        }

        [Test]
        public async Task GetActivitiesDate()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities(DateTime.Now, DateTime.Now.AddYears(-10));
            Assert.True(activities.Count > 0);
        }

        [Test]
        public async Task GetLaps()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count > 0);
            foreach (var activity in activities)
            {
                var laps = await client.Activities.GetLaps(activity.Id);
                Assert.True(laps.Count > 0);
            }
        }

        [Test]
        public async Task GetRelatedActivities()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count > 0);
            foreach (var activity in activities)
            {
                if (activity.AthleteCount > 1)
                {
                    var relatedActivities = await client.Activities.GetRelatedActivities(activity.Id, 1, 1);
                    Assert.AreEqual(1, relatedActivities.Count);
                }
            }
        }

        [Test]
        public void ParseJson()
        {
            var serializer = new JsonSerializer() { ObjectCreationHandling = ObjectCreationHandling.Reuse };
            using (var stream = TestHelper.GetResourceStream("activities.json"))
            {
                var reader = new JsonTextReader(new StreamReader(stream));
                //var result = serializer.Deserialize(reader);
                var result = serializer.Deserialize<List<ActivitySummary>>(reader);
                Assert.NotNull(result);
            }
        }

        [Test]
        public async Task GetActivityStream()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count > 0);

            var streams = await client.Activities.GetActivityStreams(activities[0].Id, StreamType.HeartRate, StreamType.LatLng);
            Assert.NotNull(streams);
            Assert.True(streams.Count > 0);
            foreach (var stream in streams)
            {
                Assert.NotNull(stream);
                Assert.NotNull(stream.Data);
            }
        }
    }
}
