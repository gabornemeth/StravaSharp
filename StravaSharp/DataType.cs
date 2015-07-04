using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public enum DataType
    {
        Fit,
        FitGz,
        Tcx,
        TcxGz,
        Gpx,
        GpxGz
    }

    public static class DataTypeExtensions
    {
        public static string ToStravaType(this DataType type)
        {
            switch (type)
            {
                case DataType.Fit:
                    return "fit";
                case DataType.FitGz:
                    return "fit.gz";
                case DataType.Tcx:
                    return "tcx";
                case DataType.TcxGz:
                    return "tcx.gz";
                case DataType.Gpx:
                    return "gpx";
                case DataType.GpxGz:
                    return "gpx.gz";
                default:
                    throw new NotSupportedException(type.ToString());
            }
        }
    }
}
