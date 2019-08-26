//
// Resource.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2017, Gabor Nemeth
//

using System;
using System.IO;
using System.Reflection;

namespace StravaSharp.Tests
{
    /// <summary>
    /// Retrieving data from embedded resources
    /// </summary>
    class Resource
    {
        public const string UserInfoJson = "StravaSharp.Tests.Files.userinfo.json";


        public static System.IO.Stream GetStream(string resourceName)
        {
            var asm = typeof(TestHelper).GetTypeInfo().Assembly;
            foreach (var name in asm.GetManifestResourceNames())
            {
                if (name.Contains(resourceName))
                    return asm.GetManifestResourceStream(name);
            }

            return null;
        }

        public static string GetText(string resourceName)
        {
            using (var stream = GetStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
