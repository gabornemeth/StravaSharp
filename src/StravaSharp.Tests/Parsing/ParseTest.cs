using System;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace StravaSharp.Tests.Parsing
{
    public class ParseTest
    {
        protected void Parse<T>(string resourceName, Action<T> assert)
        {
            var serializer = new JsonSerializer() { ObjectCreationHandling = ObjectCreationHandling.Reuse };
            using (var stream = Resource.GetStream($"Parsing.{resourceName}"))
            {
                var reader = new JsonTextReader(new StreamReader(stream));
                var result = serializer.Deserialize<T>(reader);
                Assert.NotNull(result);
                assert(result);
            }
        }
    }
}
