using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public enum ActivityType
    {
        Ride, Run, Swim, Workout, Hike, Walk,
        NordicSki, AlpineSki, BackcountrySki, IceSkate, InlineSkate, Kitesurf, RollerSki,
        WindSurf, Snowboard, SnowShoe
    }

    public static class ActivityTypeExtensions
    {
        public static string ToStravaType(this ActivityType type)
        {
            switch (type)
            {
                case ActivityType.Ride:
                    return "ride";
                case ActivityType.Run:
                    return "run";
                case ActivityType.Swim:
                    return "swim";
                case ActivityType.Workout:
                    return "workout";
                case ActivityType.Hike:
                    return "hike";
                case ActivityType.Walk:
                    return "walk";
                case ActivityType.NordicSki:
                    return "nordicski";
                case ActivityType.AlpineSki:
                    return "alpineski";
                case ActivityType.BackcountrySki:
                    return "backcountryski";
                case ActivityType.IceSkate:
                    return "iceskate";
                case ActivityType.InlineSkate:
                    return "inlineskate";
                case ActivityType.Kitesurf:
                    return "kitesurf";
                case ActivityType.RollerSki:
                    return "rollerski";
                case ActivityType.WindSurf:
                    return "windsurf";
                case ActivityType.Snowboard:
                    return "snowboard";
                case ActivityType.SnowShoe:
                    return "snowshoe";
                default:
                    throw new NotSupportedException(type.ToString());
            }
        }
    }
}
