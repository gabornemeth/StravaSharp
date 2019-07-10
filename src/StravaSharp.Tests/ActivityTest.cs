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
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    [TestFixture]
    public class ActivityTest : BaseTest
    {
        [Test]
        public async Task GetActivities()
        {
            var activities = await _client.Activities.GetAthleteActivities();
            Assert.True(activities.Count > 0);
        }

        [Test]
        public async Task GetActivitiesPage()
        {
            if (Settings.GaborTokenUnavailable)
            {
                Assert.Ignore("Not running tests requiring tokens from Settings");
            }
            const int itemsPerPage = 2;
            var activities = await _client.Activities.GetAthleteActivities(0, itemsPerPage);
            Assert.AreEqual(itemsPerPage, activities.Count);
        }

        [Test]
        public async Task GetActivitiesDate()
        {
            if (Settings.GaborTokenUnavailable)
            {
                Assert.Ignore("Not running tests requiring tokens from Settings");
            }
            var activities = await _client.Activities.GetAthleteActivities(DateTime.Now, DateTime.Now.AddYears(-10));
            Assert.True(activities.Count > 0);
        }

        [Test]
        public async Task GetLaps()
        {
            if (Settings.GaborTokenUnavailable)
            {
                Assert.Ignore("Not running tests requiring tokens from Settings");
            }
            var activities = await _client.Activities.GetAthleteActivities();
            Assert.True(activities.Count > 0);
            foreach (var activity in activities)
            {
                var laps = await _client.Activities.GetLaps(activity.Id);
                Assert.True(laps.Count > 0);
            }
        }

        [Test]
        public async Task GetActivityStream()
        {
            if (Settings.GaborTokenUnavailable)
            {
                Assert.Ignore("Not running tests requiring tokens from Settings");
            }
            var activities = await _client.Activities.GetAthleteActivities();
            Assert.True(activities.Count > 0);

            var streams = await _client.Activities.GetActivityStreams(activities[0].Id, StreamType.HeartRate, StreamType.LatLng);
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