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
        public static System.IO.Stream GetResourceStream(string resourceName)
        {
            var asm = typeof(TestHelper).GetTypeInfo().Assembly;
            return asm.GetManifestResourceStream("StravaSharp.Tests." + resourceName);
        }

        public static Client CreateStravaClient()
        {
            var authenticator = new StaticAuthenticator(Settings.AccessToken);
            return new Client(null, authenticator);
        }
    }
}
