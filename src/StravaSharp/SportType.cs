using System;
using System.Runtime.Serialization;

namespace StravaSharp
{
    /// <summary>
    /// SportType of the activity. This supersedes <see cref="ActivityType"/>.
    /// </summary>
    public enum SportType
    {
        /// <summary>
        /// AlpineSki
        /// </summary>
        [EnumMember(Value = "AlpineSki")]
        AlpineSki,

        /// <summary>
        /// BackcountrySki
        /// </summary>
        [EnumMember(Value = "BackcountrySki")]
        BackcountrySki,

        /// <summary>
        /// Badminton
        /// </summary>
        [EnumMember(Value = "Badminton")]
        Badminton,

        /// <summary>
        /// Canoeing
        /// </summary>
        [EnumMember(Value = "Canoeing")]
        Canoeing,

        /// <summary>
        /// Crossfit
        /// </summary>
        [EnumMember(Value = "Crossfit")]
        Crossfit,

        /// <summary>
        /// EBikeRide
        /// </summary>
        [EnumMember(Value = "EBikeRide")]
        EBikeRide,

        /// <summary>
        /// Elliptical
        /// </summary>
        [EnumMember(Value = "Elliptical")]
        Elliptical,

        /// <summary>
        /// EMountainBikeRide
        /// </summary>
        [EnumMember(Value = "EMountainBikeRide")]
        EMountainBikeRide,

        /// <summary>
        /// Golf
        /// </summary>
        [EnumMember(Value = "Golf")]
        Golf,

        /// <summary>
        /// GravelRide
        /// </summary>
        [EnumMember(Value = "GravelRide")]
        GravelRide,

        /// <summary>
        /// Handcycle
        /// </summary>
        [EnumMember(Value = "Handcycle")]
        Handcycle,

        /// <summary>
        /// HighIntensityIntervalTraining
        /// </summary>
        [EnumMember(Value = "HighIntensityIntervalTraining")]
        HighIntensityIntervalTraining,

        /// <summary>
        /// Hike
        /// </summary>
        [EnumMember(Value = "Hike")]
        Hike,

        /// <summary>
        /// IceSkate
        /// </summary>
        [EnumMember(Value = "IceSkate")]
        IceSkate,

        /// <summary>
        /// InlineSkate
        /// </summary>
        [EnumMember(Value = "InlineSkate")]
        InlineSkate,

        /// <summary>
        /// Kayaking
        /// </summary>
        [EnumMember(Value = "Kayaking")]
        Kayaking,

        /// <summary>
        /// Kitesurf
        /// </summary>
        [EnumMember(Value = "Kitesurf")]
        Kitesurf,

        /// <summary>
        /// MountainBikeRide
        /// </summary>
        [EnumMember(Value = "MountainBikeRide")]
        MountainBikeRide,

        /// <summary>
        /// NordicSki
        /// </summary>
        [EnumMember(Value = "NordicSki")]
        NordicSki,

        /// <summary>
        /// Pickleball
        /// </summary>
        [EnumMember(Value = "Pickleball")]
        Pickleball,

        /// <summary>
        /// Pilates
        /// </summary>
        [EnumMember(Value = "Pilates")]
        Pilates,

        /// <summary>
        /// Racquetball
        /// </summary>
        [EnumMember(Value = "Racquetball")]
        Racquetball,

        /// <summary>
        /// Ride
        /// </summary>
        [EnumMember(Value = "Ride")]
        Ride,

        /// <summary>
        /// RockClimbing
        /// </summary>
        [EnumMember(Value = "RockClimbing")]
        RockClimbing,

        /// <summary>
        /// RollerSki
        /// </summary>
        [EnumMember(Value = "RollerSki")]
        RollerSki,

        /// <summary>
        /// Rowing
        /// </summary>
        [EnumMember(Value = "Rowing")]
        Rowing,

        /// <summary>
        /// Run
        /// </summary>
        [EnumMember(Value = "Run")]
        Run,

        /// <summary>
        /// Sail
        /// </summary>
        [EnumMember(Value = "Sail")]
        Sail,

        /// <summary>
        /// Skateboard
        /// </summary>
        [EnumMember(Value = "Skateboard")]
        Skateboard,

        /// <summary>
        /// Snowboard
        /// </summary>
        [EnumMember(Value = "Snowboard")]
        Snowboard,

        /// <summary>
        /// Snowshoe
        /// </summary>
        [EnumMember(Value = "Snowshoe")]
        Snowshoe,

        /// <summary>
        /// Soccer
        /// </summary>
        [EnumMember(Value = "Soccer")]
        Soccer,

        /// <summary>
        /// Squash
        /// </summary>
        [EnumMember(Value = "Squash")]
        Squash,

        /// <summary>
        /// StairStepper
        /// </summary>
        [EnumMember(Value = "StairStepper")]
        StairStepper,

        /// <summary>
        /// StandUpPaddling
        /// </summary>
        [EnumMember(Value = "StandUpPaddling")]
        StandUpPaddling,

        /// <summary>
        /// Surfing
        /// </summary>
        [EnumMember(Value = "Surfing")]
        Surfing,

        /// <summary>
        /// Swim
        /// </summary>
        [EnumMember(Value = "Swim")]
        Swim,

        /// <summary>
        /// TableTennis
        /// </summary>
        [EnumMember(Value = "TableTennis")]
        TableTennis,

        /// <summary>
        /// Tennis
        /// </summary>
        [EnumMember(Value = "Tennis")]
        Tennis,

        /// <summary>
        /// TrailRun
        /// </summary>
        [EnumMember(Value = "TrailRun")]
        TrailRun,

        /// <summary>
        /// Velomobile
        /// </summary>
        [EnumMember(Value = "Velomobile")]
        Velomobile,

        /// <summary>
        /// VirtualRide
        /// </summary>
        [EnumMember(Value = "VirtualRide")]
        VirtualRide,

        /// <summary>
        /// VirtualRow
        /// </summary>
        [EnumMember(Value = "VirtualRow")]
        VirtualRow,

        /// <summary>
        /// VirtualRun
        /// </summary>
        [EnumMember(Value = "VirtualRun")]
        VirtualRun,

        /// <summary>
        /// Walk
        /// </summary>
        [EnumMember(Value = "Walk")]
        Walk,

        /// <summary>
        /// WeightTraining
        /// </summary>
        [EnumMember(Value = "WeightTraining")]
        WeightTraining,

        /// <summary>
        /// Wheelchair
        /// </summary>
        [EnumMember(Value = "Wheelchair")]
        Wheelchair,

        /// <summary>
        /// Windsurf
        /// </summary>
        [EnumMember(Value = "Windsurf")]
        Windsurf,

        /// <summary>
        /// Workout
        /// </summary>
        [EnumMember(Value = "Workout")]
        Workout,

        /// <summary>
        /// Yoga
        /// </summary>
        [EnumMember(Value = "Yoga")]
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
