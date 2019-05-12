using System;
using System.Runtime.Serialization;

namespace StravaSharp
{
    /// <summary>
    /// Type of the activity
    /// </summary>
    public enum ActivityType
    {
		/// <summary>
		/// Ride
		/// </summary>
        [EnumMember(Value = "ride")]
        Ride,
		/// <summary>
		/// Running
		/// </summary>
        [EnumMember(Value = "run")]
        Run,
		/// <summary>
		/// Swimming
		/// </summary>
        [EnumMember(Value = "swim")]
        Swim,
		/// <summary>
		/// Hiking
		/// </summary>
        [EnumMember(Value = "hike")]
        Hike,
		/// <summary>
		/// Walking
		/// </summary>
        [EnumMember(Value = "walk")]
        Walk,
		/// <summary>
		/// Alpine skiing
		/// </summary>
        [EnumMember(Value = "alpineski")]
        AlpineSki,
		/// <summary>
		/// Backcountry skiing
		/// </summary>
        [EnumMember(Value = "backcountryski")]
        BackcountrySki,
		/// <summary>
		/// Canoeing
		/// </summary>
        [EnumMember(Value = "canoeing")]
        Canoeing,
		/// <summary>
		/// Crossfit
		/// </summary>
        [EnumMember(Value = "crossfit")]
        Crossfit,
		/// <summary>
		/// E-bike cycling
		/// </summary>
        [EnumMember(Value = "ebikeride")]
        EBikeRide,
		/// <summary>
		/// Elliptical trainer
		/// </summary>
        [EnumMember(Value = "elliptical")]
        Elliptical,
		/// <summary>
		/// Ice skating
		/// </summary>
        [EnumMember(Value = "iceskate")]
        IceSkate,
		/// <summary>
		/// Inline skating
		/// </summary>
        [EnumMember(Value = "inlineskate")]
        InlineSkate,
		/// <summary>
		/// Kayaking
		/// </summary>
        [EnumMember(Value = "kayaking")]
        Kayaking,
		/// <summary>
		/// Kitesurfing
		/// </summary>
        [EnumMember(Value = "kitesurf")]
        Kitesurf,
		/// <summary>
		/// Nordic skiing
		/// </summary>
        [EnumMember(Value = "nordicski")]
        NordicSki,
		/// <summary>
		/// Rock climbing
		/// </summary>
        [EnumMember(Value = "rockclimbing")]
        RockClimbing,
		/// <summary>
		/// Roller skiing
		/// </summary>
        [EnumMember(Value = "rollerski")]
        RollerSki,
		/// <summary>
		/// Rowing
		/// </summary>
        [EnumMember(Value = "rowing")]
        Rowing,
		/// <summary>
		/// Snowboarding
		/// </summary>
        [EnumMember(Value = "snowboard")]
        Snowboard,
		/// <summary>
		/// Snowshoe walking
		/// </summary>
        [EnumMember(Value = "snowshoe")]
        SnowShoe,
		/// <summary>
		/// Stair stepping
		/// </summary>
        [EnumMember(Value = "stairstepper")]
        StairStepper,
		/// <summary>
		/// Stand up paddling (SUP)
		/// </summary>
        [EnumMember(Value = "standuppaddling")]
        StandUpPaddling,
		/// <summary>
		/// Surfing
		/// </summary>
        [EnumMember(Value = "surfing")]
        Surfing,
		/// <summary>
		/// Virtual cycling
		/// </summary>
        [EnumMember(Value = "virtualride")]
        VirtualRide,
		/// <summary>
		/// Weight training
		/// </summary>
        [EnumMember(Value = "weighttraining")]
        WeightTraining,
		/// <summary>
		/// Wind surfing
		/// </summary>
        [EnumMember(Value = "windsurf")]
        WindSurf,
		/// <summary>
		/// General workout
		/// </summary>
        [EnumMember(Value = "workout")]
        Workout,
		/// <summary>
		/// Yoga
		/// </summary>
        [EnumMember(Value = "yoga")]
        Yoga,

        // LEGACY types: not supported officially by Strava but can still exist and Strava's own software are prepared for them as well.

        /// <summary>
        /// [OBSOLETE] WaterSport
        /// </summary>
        [Obsolete]
        [EnumMember(Value = "watersport")]
        WaterSport
    }
}
