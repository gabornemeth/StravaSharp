using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace StravaSharp
{
    /// <summary>
    /// Base class of Strava objects
    /// </summary>
    public class StravaObject<T>
    {
        [JsonProperty("id")]
        public T Id { get; internal set; }
        [JsonProperty("resource_state")]
        public int ResourceState { get; internal set; }
    }
}
