//
// SegmentTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//
        
using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StravaSharp.Tests
{
    [TestFixture]
    public class SegmentTest
    {
        [Test]
        public async Task GetSegments()
        {
            // retrieve segments near Zalaegerszeg, Hungary
            var client = TestHelper.CreateStravaClient();
            var southWest = new LatLng { Latitude = 46.828100f, Longitude = 16.781540f };
            var northEast = new LatLng { Latitude = 46.859259f, Longitude = 16.832550f };
            var segments = await client.Segments.Explore(southWest, northEast);
            Assert.NotNull(segments);
            Assert.True(segments.Count > 1);
            // check the first segment in detail
            foreach (var segment in segments)
            {
                var segmentDetails = await client.Segments.Get(segment.Id);
                Assert.NotNull(segmentDetails);
                Assert.NotNull(segmentDetails.Map);
            }
        }
    }
}
