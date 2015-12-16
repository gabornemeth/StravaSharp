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
            foreach (var name in asm.GetManifestResourceNames())
            {
                if (name.Contains(resourceName))
                    return asm.GetManifestResourceStream(name);
            }

            return null;
        }

        public static Client CreateStravaClient()
        {
            var authenticator = new StaticAuthenticator(Settings.AccessToken);
            return new Client(authenticator);
        }
    }
}
