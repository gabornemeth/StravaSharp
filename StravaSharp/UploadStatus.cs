//
// UploadResult.cs
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
    /// Result of the upload procedure
    /// This can be used to further check the status of uploading
    /// </summary>
    public class UploadStatus
    {
        [JsonProperty("id")]
        public int Id { get; internal set; }
        [JsonProperty("external_id")]
        public string ExternalId { get; internal set; }
        [JsonProperty("activity_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ActivityId { get; internal set; }
        [JsonProperty("status")]
        public string Status { get; internal set; }
        [JsonProperty("error")]
        public string Error { get; internal set; }

        [JsonIgnore]
        public bool IsReady
        {
            get
            {
                return Status == "Your activity is ready.";
            }
        }
    }
}
