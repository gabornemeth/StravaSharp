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
    public class ClubTest
    {
        [Test]
        public async Task GetClub()
        {
            if (!Settings.SkipAsPassedAccessTokenTests)
            {
                var client = TestHelper.StravaClientFromSettings();
                var club = await client.Clubs.Get(Settings.ClubId);
                Assert.NotNull(club);
                Assert.True(string.IsNullOrEmpty(club.Name) == false);
                Assert.True(string.IsNullOrEmpty(club.Country) == false);
            }
        }
        [Test]
        public async Task GetClubMembers()
        {
            if (!Settings.SkipAsPassedAccessTokenTests)
            {
                var client = TestHelper.StravaClientFromSettings();
                var members = await client.Clubs.GetMembers(Settings.ClubId);
                Assert.NotNull(members);
                Assert.True(members.Count > 0);
            }
        }
        [Test]
        public async Task GetClubAdmins()
        {
            if (!Settings.SkipAsPassedAccessTokenTests)
            {
                var client = TestHelper.StravaClientFromSettings();
                var admins = await client.Clubs.GetAdmins(Settings.ClubId);
                Assert.NotNull(admins);
                Assert.True(admins.Count > 0);
            }
        }
    }
}
