using System;
using System.Runtime.Serialization;

namespace StravaSharp
{
    /// <summary>
    /// Type of data for upload
    /// </summary>
    public enum DataType
    {
        [EnumMember(Value="fit")]
        Fit,
        [EnumMember(Value = "fit.gz")]
        FitGz,
        [EnumMember(Value = "tcx")]
        Tcx,
        [EnumMember(Value = "tcx.gz")]
        TcxGz,
        [EnumMember(Value = "gpx")]
        Gpx,
        [EnumMember(Value = "gpx.gz")]
        GpxGz
    }
}
