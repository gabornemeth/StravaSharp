//
// SegmentTest.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using NUnit.Framework;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    public class BaseTest
    {
        protected IStravaClient _client;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _client = await TestHelper.StravaClientFromSettings();
        }

    }
}
