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
            Assert.That(activities, Is.Not.Null);
            Assert.That(activities.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task GetActivitiesPage()
        {
            var client = await TestHelper.CreateStravaClient();
            const int itemsPerPage = 2;
            var activities = await client.Activities.GetAthleteActivities(0, itemsPerPage);
            Assert.That(activities.Count(), Is.EqualTo(itemsPerPage));
        }

        [Test]
        public async Task GetActivitiesDate()
        {
            var client = await TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities(DateTime.Now, DateTime.Now.AddYears(-10));
            Assert.That(activities.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task GetLaps()
        {
            var client = await TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.That(activities.Count(), Is.GreaterThan(0));
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
            Assert.That(activities.Count(), Is.GreaterThan(0));

            var streams = await client.Activities.GetActivityStreams(activities.First().Id, StreamType.HeartRate, StreamType.LatLng);
            Assert.That(streams, Is.Not.Null);
            Assert.That(streams.Count(), Is.GreaterThan(0));
            foreach (var stream in streams)
            {
                Assert.That(stream?.Data, Is.Not.Null);
            }
        }

        [Test]
        public async Task GetActivityZones()
        {
            var client = await TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.That(activities.Count(), Is.GreaterThan(0));

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
            Assert.That(activity, Is.Not.Null);

            // Act
            var updatedActivity = await client.Activities.Update(activityId, new UpdatableActivity
            {
                Name = activity.Name == "Evening Ride" ? "Ride" : "Evening Ride",
                Description = string.IsNullOrEmpty(activity.Description) ? "Testing update" : ""
            });

            // Assert
            updatedActivity.Name.Should().NotBe(activity.Name);
            updatedActivity.Description.Should().NotBe(activity.Description);
        }
    }
}
