using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public class Photo: StravaObject<string>
    {
        [JsonProperty("source")]
        public int Source { get; internal set; }
        [JsonProperty("unique_id")]
        public string UniqueID { get; internal set; }
        [JsonProperty("urls")]
        public PhotoUrl Urls { get; internal set; }
    }

    public struct PhotoUrl
    {
        [JsonProperty("100")]
        public string Small;
        [JsonProperty("600")]
        public string Medium;
    }

    public class PhotoInfo
    {
        [JsonProperty("primary")]
        public Photo Primary { get; internal set; }
        [JsonProperty("use_primary_photo")]
        public bool UsePrimaryPhoto { get; internal set; }
        [JsonProperty("count")]
        public int Count { get; internal set; }
    }
}
