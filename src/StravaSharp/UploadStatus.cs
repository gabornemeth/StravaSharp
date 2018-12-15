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
		/// <summary>
		/// Identifier of the upload process
		/// </summary>
		/// <value>The identifier.</value>
        [JsonProperty("id")]
        public long Id { get; internal set; }
        
		[JsonProperty("external_id")]
        public string ExternalId { get; internal set; }

		/// <summary>
		/// Identifier of the activity
		/// </summary>
        [JsonProperty("activity_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ActivityId { get; internal set; }

		/// <summary>
		/// Describes the error, possible values:
		///  ‘Your activity is still being processed.’,
		///  ‘The created activity has been deleted.’,
		///  ‘There was an error processing your activity.’,
		///  ‘Your activity is ready.’
		/// </summary>
		/// <value>The status.</value>
        [JsonProperty("status")]
        public string Status { get; internal set; }

		/// <summary>
		/// If there was an error during processing, this will contain a human a human readable error message that may include escaped HTML
		/// </summary>
		/// <value>The error.</value>
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
