using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public interface IMap
    {
        /// <summary>
        /// Polyline with all points
        /// </summary>
        string Polyline { get; }

        /// <summary>
        /// Summary polyline
        /// </summary>
        string SummaryPolyline { get; }

    }
}
