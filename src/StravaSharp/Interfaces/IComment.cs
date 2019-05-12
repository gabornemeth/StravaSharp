using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public interface IComment
    {
        long Id { get; }
        /// <summary>
        /// Identifier of the parent activity
        /// </summary>
        long ActivityId { get; }
        /// <summary>
        /// The actual comment
        /// </summary>
        string Text { get; }
        /// <summary>
        /// summary representation of the commenting athlete
        /// </summary>
        IAthleteSummary Athlete { get; }
        /// <summary>
        /// Time of creation
        /// </summary>
        DateTime CreateTime { get; }
    }
}
