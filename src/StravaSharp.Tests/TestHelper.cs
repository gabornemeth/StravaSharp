//
// TestHelper.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//
        
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace StravaSharp.Tests
{
    public static class TestHelper
    {
        public static Client CreateStravaClient()
        {
            var authenticator = new TestAuthenticator(Settings.AccessToken);
            return new Client(authenticator);
        }

        public static Client CreateFakeStravaClient()
        {
            var authenticator = new TestAuthenticator(Settings.AccessToken);
            return new FakeClient(authenticator);
        }
    }
}
