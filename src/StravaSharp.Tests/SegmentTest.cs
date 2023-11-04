﻿//
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
    public class Test
    {
        protected async Task GoOnIfPremium(Client client, Func<Task> action)
        {
            var currentUser = await client.Athletes.GetCurrent();
            if (currentUser.Premium)
            {
                await action();
            }
        }
    }

    [TestFixture]
    public class SegmentTest : Test
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
                Assert.NotNull(segmentDetails.Map.Polyline);
                // retrieve each coordinate of the segment's polylinef
                var points = SharpGeo.Google.PolylineEncoder.Decode(segmentDetails.Map.Polyline);
                Assert.NotNull(points);
                Assert.True(points.Count > 0);
                foreach (var point in points)
                    Assert.True(point.IsEmpty == false);
            }
        }

        SegmentSummary GetTestSegment(IEnumerable<SegmentSummary> segments)
        {
            var rnd = new Random();
            var segmentForTesting = segments.ElementAt(rnd.Next(segments.Count()));
            Assert.NotNull(segmentForTesting);
            return segmentForTesting;
        }

        [Test]
        public async Task GetEfforts()
        {
            var segments = await RetrieveSegments();
            var segment = GetTestSegment(segments);

            await GoOnIfPremium(_client, async () =>
            {
                var efforts = (await _client.Segments.GetEfforts(segment.Id, 1, 2)).ToArray();
                Assert.GreaterOrEqual(efforts.Length, 1);
                Assert.LessOrEqual(efforts.Length, 2);

                foreach (var effort in efforts)
                {
                    Assert.NotNull(effort.Activity);
                    Assert.NotNull(effort.Athlete);
                    Assert.NotNull(effort.Segment);
                }
            });
        }

        [Test]
        public async Task GetSegmentStreams()
        {
            var segments = await RetrieveSegments();
            var segment = segments[0];

            var streams = await _client.Segments.GetSegmentStreams(segment, StreamType.Distance, StreamType.LatLng);
            Assert.NotNull(streams);
            Assert.True(streams.Count > 0);
            foreach (var stream in streams)
            {
                Assert.NotNull(stream);
                Assert.NotNull(stream.Data);
            }
        }

        private async Task<SegmentEffort> RetrieveEffort(SegmentSummary segment)
        {
            var efforts = (await _client.Segments.GetEfforts(segment.Id, 1, 2)).ToArray();
            Assert.GreaterOrEqual(efforts.Length, 1);
            Assert.LessOrEqual(efforts.Length, 2);
            return efforts[0];
        }

        [Test]
        public async Task GetEffortStreams()
        {
            var segments = await RetrieveSegments();
            var segment = GetTestSegment(segments);
            await GoOnIfPremium(_client, async () =>
            {
                var effort = await RetrieveEffort(segment);

                var streams = await _client.Segments.GetEffortStreams(effort, StreamType.Distance, StreamType.LatLng);
                Assert.NotNull(streams);
                Assert.True(streams.Count > 0);
                foreach (var stream in streams)
                {
                    Assert.NotNull(stream);
                    Assert.NotNull(stream.Data);
                }
            });
        }
    }
}
