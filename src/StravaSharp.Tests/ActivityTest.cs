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
            if (!Settings.SkipAsPassedAccessTokenTests)
            {
                var client = TestHelper.StravaClientFromSettings();
                var activities = await client.Activities.GetAthleteActivities();
                Assert.True(activities.Count > 0);
            }
        }

        [Test]
        public async Task GetActivitiesPage()
        {
            if (!Settings.SkipAsPassedAccessTokenTests)
            {
                var client = TestHelper.StravaClientFromSettings();
                const int itemsPerPage = 2;
                var activities = await client.Activities.GetAthleteActivities(0, itemsPerPage);
                Assert.AreEqual(itemsPerPage, activities.Count);
            }
        }

        [Test]
        public async Task GetActivitiesDate()
        {
            if (!Settings.SkipAsPassedAccessTokenTests)
            {
                var client = TestHelper.StravaClientFromSettings();
                var activities = await client.Activities.GetAthleteActivities(DateTime.Now, DateTime.Now.AddYears(-10));
                Assert.True(activities.Count > 0);
            }
        }

        [Test]
        public async Task GetLaps()
        {
            if (!Settings.SkipAsPassedAccessTokenTests)
            {
                var client = TestHelper.StravaClientFromSettings();
                var activities = await client.Activities.GetAthleteActivities();
                Assert.True(activities.Count > 0);
                foreach (var activity in activities)
                {
                    var laps = await client.Activities.GetLaps(activity.Id);
                    Assert.True(laps.Count > 0);
                }
            }
        }

        [Test]
        public async Task GetActivityStream()
        {
            if (!Settings.SkipAsPassedAccessTokenTests)
            {
                var client = TestHelper.StravaClientFromSettings();
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
}