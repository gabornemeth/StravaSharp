//
// Map.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//
        
using System;
using Newtonsoft.Json;

namespace StravaSharp
{
    /// <summary>
    /// Detailed representation of the route
    /// </summary>
    public class Map : StravaObject<string>
    {
        /// <summary>
        /// Polyline with all points
        /// </summary>
        [JsonProperty("polyline")]
        public string Polyline { get; internal set; }

        /// <summary>
        /// Summary polyline
        /// </summary>
        [JsonProperty("summary_polyline")]
        public string SummaryPolyline { get; internal set; }
    }
}
