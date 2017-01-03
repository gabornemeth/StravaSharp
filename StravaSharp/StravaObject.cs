//
// StravaObject.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//
        
using Newtonsoft.Json;

namespace StravaSharp
{
    /// <summary>
    /// Base class of Strava objects
    /// T: identifier type
    /// </summary>
    public class StravaObject<T>
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [JsonProperty("id")]
        public T Id { get; internal set; }
        /// <summary>
        /// Resource state
        /// </summary>
        [JsonProperty("resource_state")]
        public ResourceState ResourceState { get; internal set; }
    }
}
