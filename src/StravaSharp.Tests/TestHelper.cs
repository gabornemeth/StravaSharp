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
        public static StravaClient CreateStravaClient()
        {
            var authenticator = new TestAuthenticator(Settings.AccessToken);
            return new StravaClient(authenticator);
        }
    }
}
