using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    /// <summary>
    /// Result of the upload procedure
    /// This can be used to further check the status of uploading
    /// </summary>
    public class UploadResult
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }
        [JsonProperty("external_id")]
        public string ExternalId { get; internal set; }
        [JsonProperty("activity_id")]
        public string ActivityId { get; internal set; }
        [JsonProperty("status")]
        public string Status { get; internal set; }
        [JsonProperty("error")]
        public string Error { get; internal set; }
    }
}
