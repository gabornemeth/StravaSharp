//
// ClubTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2016, Gabor Nemeth
//

using NUnit.Framework;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    [TestFixture]
    public class ClubTest : BaseTest
    {
        [Test]
        public async Task GetClub()
        {
            var club = await _client.Clubs.Get(Settings.ClubId);
            Assert.NotNull(club);
            Assert.True(string.IsNullOrEmpty(club.Name) == false);
            Assert.True(string.IsNullOrEmpty(club.Country) == false);
        }
        [Test]
        public async Task GetClubMembers()
        {
            var members = await _client.Clubs.GetMembers(Settings.ClubId);
            Assert.NotNull(members);
            Assert.True(Settings.GaborTokenUnavailable || members.Count > 0);
        }
        [Test]
        public async Task GetClubAdmins()
        {
            var admins = await _client.Clubs.GetAdmins(Settings.ClubId);
            Assert.NotNull(admins);
            Assert.True(admins.Count > 0);
        }
    }
}
