//
// Enums.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;

namespace StravaSharp
{
    public enum Gender
    {
        /// <summary>
        /// Male
        /// </summary>
        Male,
        /// <summary>
        /// Female
        /// </summary>
        Female
    }

    /// <summary>
    /// Age group
    /// </summary>
    public enum AgeGroup
    {
        /// <summary>
        /// 0 - 24
        /// </summary>
        From0To24,
        /// <summary>
        /// 25 - 34
        /// </summary>
        From25To34,
        /// <summary>
        /// 35 - 44
        /// </summary>
        From35To44,
        /// <summary>
        /// 45 to 54
        /// </summary>
        From45To54,
        /// <summary>
        /// 55 to 64
        /// </summary>
        From55To64,
        /// <summary>
        /// 64 plus
        /// </summary>
        From64
    }

    public static class EnumExtensions
    {
        public static string ToStravaString(this AgeGroup ageGroup)
        {
            switch (ageGroup)
            {
                case AgeGroup.From0To24:
                    return "0_24";
                case AgeGroup.From25To34:
                    return "25_34";
                case AgeGroup.From35To44:
                    return "35_44";
                case AgeGroup.From45To54:
                    return "45_54";
                case AgeGroup.From55To64:
                    return "55_64";
                case AgeGroup.From64:
                    return "64Plus";
                default:
                    throw new NotSupportedException(string.Format("Unknown age group: {0}", ageGroup));
            }
        }

        public static string ToStravaString(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return "F";
                case Gender.Male:
                    return "M";
                default:
                    throw new NotSupportedException(string.Format("Unknown gender: {0}", gender));
            }
        }
    }
}
