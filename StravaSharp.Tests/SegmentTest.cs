//
// SegmentTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StravaSharp.Tests
{
    [TestFixture]
    public class SegmentTest
    {
        private Client _client;

        [SetUp]
        public void Setup()
        {
            _client = TestHelper.CreateStravaClient();
        }

        private async Task<SegmentSummary[]> RetrieveSegments()
        {
            // retrieve segments near Zalaegerszeg, Hungary
            var southWest = new LatLng { Latitude = 46.828100f, Longitude = 16.781540f };
            var northEast = new LatLng { Latitude = 46.859259f, Longitude = 16.832550f };
            var segments = (await _client.Segments.Explore(southWest, northEast)).ToArray();
            Assert.NotNull(segments);
            Assert.True(segments.Length > 1);
            return segments;
        }

        [Test]
        public async Task GetSegments()
        {
            // retrieve segments near Zalaegerszeg, Hungary
            var segments = await RetrieveSegments();
            // check each segment in detail
            foreach (var segment in segments)
            {
                var segmentDetails = await _client.Segments.Get(segment.Id);
                Assert.NotNull(segmentDetails);
                Assert.NotNull(segmentDetails.Map);
                // retrieve each coordinate of the segment's polylinef
                var points = PolylineDecoder.DecodePolylinePoints(segmentDetails.Map.Polyline);
                Assert.NotNull(points);
                Assert.True(points.Count > 0);
                foreach (var point in points)
                    Assert.True(point.IsEmpty() == false);
            }
        }

        [Test]
        public async Task GetEfforts()
        {
            var segments = await RetrieveSegments();
            var segment = segments[0];
            var efforts = (await _client.Segments.GetEfforts(segment.Id, 1, 2)).ToArray();
            Assert.AreEqual(2, efforts.Length);
            foreach (var effort in efforts)
            {
                Assert.NotNull(effort.Activity);
                Assert.NotNull(effort.Athlete);
                Assert.NotNull(effort.Segment);
            }
        }

        [Test]
        public async Task GetLeaderboard()
        {
            var segments = await RetrieveSegments();
            var segment = segments[0];
            var leaderboard = await _client.Segments.GetLeaderboard(segment.Id, null, null);
            Assert.NotNull(leaderboard);
            //Assert.AreEqual(leaderboard.EntryCount, leaderboard.Entries.Count);
            //Assert.AreEqual(2, efforts.Length);
            //foreach (var effort in efforts)
            //{
            //    Assert.NotNull(effort.Activity);
            //    Assert.NotNull(effort.Athlete);
            //    Assert.NotNull(effort.Segment);
            //}
        }

    }
}
