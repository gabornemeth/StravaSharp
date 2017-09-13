//
// ClubTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2016, Gabor Nemeth
//

using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    [TestFixture]
    public class ClubTest
    {
        [Test]
        public async Task GetClub()
        {
            var client = TestHelper.CreateStravaClient();
            var club = await client.Clubs.Get(Settings.ClubId);
            Assert.NotNull(club);
            Assert.True(string.IsNullOrEmpty(club.Name) == false);
            Assert.True(string.IsNullOrEmpty(club.Country) == false);
        }

        [Test]
        public async Task GetClubMembers()
        {
            var client = TestHelper.CreateStravaClient();
            var members = await client.Clubs.GetMembers(Settings.ClubId);
            Assert.NotNull(members);
            Assert.True(members.Count > 0);
        }

        [Test]
        public async Task GetClubAdmins()
        {
            var client = TestHelper.CreateStravaClient();
            var admins = await client.Clubs.GetAdmins(Settings.ClubId);
            Assert.NotNull(admins);
            Assert.True(admins.Count > 0);
        }
    }
}
