//
// ClubTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2016, Gabor Nemeth
//

using FluentAssertions;
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
            var client = await TestHelper.CreateStravaClient();
            var club = await client.Clubs.Get(Settings.ClubId);
            
            club.Should().NotBeNull();
            club.Name.Should().NotBeNullOrEmpty();
            club.Country.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task GetClubMembers()
        {
            var client = await TestHelper.CreateStravaClient();
            var members = await client.Clubs.GetMembers(Settings.ClubId);
            
            members.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task GetClubAdmins()
        {
            var client = await TestHelper.CreateStravaClient();
            var admins = await client.Clubs.GetAdmins(Settings.ClubId);
            
            admins.Should().NotBeNullOrEmpty();
        }
    }
}
