using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    /// <summary>
    /// Athlete tests
    /// Access token is read-write for gabor.nemeth.dev@gmail.com (Extrava Test Account)
    /// </summary>
    [TestFixture]
    public class AthleteTest
    {
        private const int TestAthleteId = 4076458; // gabor.nemeth1982@gmail.com

        [Test]
        public async Task GetCurrentAthlete()
        {
            var client = TestHelper.CreateStravaClient();
            var athlete = await client.Athletes.GetCurrent();
            Assert.NotNull(athlete);
            Assert.AreEqual("gabor.nemeth.dev@gmail.com", athlete.Email);
            Assert.AreEqual("Extrava", athlete.FirstName);
        }
        
        [Test]
        public async Task GetAthlete()
        {
            var client = TestHelper.CreateStravaClient();
            var athlete = await client.Athletes.Get(6632444);
            Assert.NotNull(athlete);
            Assert.AreEqual("gabor.nemeth.dev@gmail.com", athlete.Email);
        }

        [Test]
        public async Task GetFriendsOfCurrentAthlete()
        {
            var client = TestHelper.CreateStravaClient();
            var friends = await client.Athletes.GetFriendsOfCurrent();
            Assert.NotNull(friends);
            Assert.AreEqual(1, friends.Count());
        }

        [Test]
        public async Task GetFollowersOfCurrentAthlete()
        {
            var client = TestHelper.CreateStravaClient();
            var followers = await client.Athletes.GetFollowersOfCurrent();
            Assert.NotNull(followers);
            Assert.AreEqual(1, followers.Count());
        }

        [Test]
        public async Task GetFriends()
        {
            var client = TestHelper.CreateStravaClient();
            var friends = await client.Athletes.GetFriends(TestAthleteId);
            Assert.NotNull(friends);
            Assert.True(friends.Count() > 0);
        }

        [Test]
        public async Task GetFollowersOfAnAthlete()
        {
            var client = TestHelper.CreateStravaClient();
            // gabor.nemeth1982@gmail.com
            var followers = await client.Athletes.GetFollowers(TestAthleteId);
            Assert.NotNull(followers);
            Assert.True(followers.Count() > 0);
        }

        [Test]
        public async Task GetBothFollowing()
        {
            var client = TestHelper.CreateStravaClient();
            var athletes = await client.Athletes.GetFriendsOfBoth(TestAthleteId);
            Assert.NotNull(athletes);
            Assert.AreEqual(0, athletes.Count());
        }
    }
}
