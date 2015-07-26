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
    public class ActivityTest
    {
        [Test]
        public async Task GetActivities()
        {
            //var config = new RestSharp.Portable.Authenticators.OAuth2.Configuration.RuntimeClientConfiguration();
            //config.IsEnabled = false;
            //config.ClientId = clientId;
            //config.ClientSecret = clientSecret;
            //config.RedirectUri = redirectUrl.AbsoluteUri;
            //config.Scope = scope;
            //var client = new StravaClient(new RequestFactory(), config);

            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count > 0);
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
        public async Task GetStream()
        {
            var client = TestHelper.CreateStravaClient();
            var activities = await client.Activities.GetAthleteActivities();
            Assert.True(activities.Count > 0);

            var streams = await client.GetActivityStreams(activities[0].Id, StreamType.HeartRate, StreamType.LatLng);
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
