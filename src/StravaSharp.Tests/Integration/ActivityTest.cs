//
// ActivityTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StravaSharp.Tests.Integration
{
    [TestFixture]
    public class ActivityTest : Test
    {
        [Test]
        public async Task GetActivities()
        {
            var client = await TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.NotNull(activities);
            Assert.True(activities.Count() > 0);
        }

        [Test]
        public async Task GetActivitiesPage()
        {
            var client = await TestHelper.CreateStravaClient();
            const int itemsPerPage = 2;
            var activities = await client.Activities.GetAthleteActivities(0, itemsPerPage);
            Assert.AreEqual(itemsPerPage, activities.Count());
        }

        [Test]
        public async Task GetActivitiesDate()
        {
            var client = await TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities(DateTime.Now, DateTime.Now.AddYears(-10));
            Assert.True(activities.Count() > 0);
        }

        [Test]
        public async Task GetLaps()
        {
            var client = await TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count() > 0);
            foreach (var activity in activities)
            {
                var laps = await client.Activities.GetLaps(activity.Id);
                laps.Should().NotBeNullOrEmpty();
            }
        }

        [Test]
        public async Task GetActivityStream()
        {
            var client = await TestHelper.CreateStravaClient();
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
            var client = await TestHelper.CreateStravaClient();
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
        public async Task UpdateActivity()
        {
            // Arrange
            var activityId = 1830726355;
            var client = await TestHelper.CreateStravaClient();
            var activity = await client.Activities.Get(activityId, false);
            Assert.NotNull(activity);

            // Act
            await client.Activities.Update(activityId, new UpdatableActivity
            {
                Name = activity.Name == "Evening Ride" ? "Ride" : "Evening Ride",
                Description = string.IsNullOrEmpty(activity.Description) ? "Testing update" : ""
            });
            var updatedActivity = await client.Activities.Get(activityId, false);

            // Assert
            updatedActivity.Name.Should().NotBe(activity.Name);
            updatedActivity.Description.Should().NotBe(activity.Description);
        }
    }
}
