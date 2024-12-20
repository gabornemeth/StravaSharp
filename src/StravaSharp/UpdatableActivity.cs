using Newtonsoft.Json;

namespace StravaSharp
{
    public class UpdatableActivity
    {
        /// <summary>
        /// Whether this activity is a commute
        /// </summary>
        [JsonProperty("commute", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Commute { get; set; }

        /// <summary>
        /// Whether this activity was recorded on a training machine 
        /// </summary>
        [JsonProperty("trainer", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Trainer { get; set; }

        /// <summary>
        /// Whether this activity is muted
        /// </summary>
        [JsonProperty("hide_from_home", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HideFromHome { get; set; }

        /// <summary>
        /// The description of the activity 
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; internal set; }

        /// <summary>
        /// The name of the activity 
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; internal set; }

        /// <summary>
        /// Deprecated. Prefer to use sport_type. In a request where both Type and SportType are present, this field will be ignored.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public ActivityType? Type { get; set; }

        /// <summary>
        /// The SportType of the activity. In a request where both Type and SportType are present, this field will be used.
        /// </summary>
        [JsonProperty("sport_type", NullValueHandling = NullValueHandling.Ignore)]
        public SportType? SportType { get; set; }

        /// <summary>
        /// Identifier for the gear associated with the activity. "none" clears gear from activity 
        /// </summary>
        [JsonProperty("gear_id", NullValueHandling = NullValueHandling.Ignore)]
        public string GearId { get; set; }
    }

}
