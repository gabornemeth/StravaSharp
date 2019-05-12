namespace StravaSharp
{
    public interface IStream
    {
        /// <summary>
        /// Type of the stream
        /// </summary>
        StreamType Type { get; }

        /// <summary>
        /// Array of stream values
        /// </summary>
        object[] Data { get; }

        /// <summary>
        /// Series type used for down sampling, will be present even if not used
        /// </summary>
        SeriesType SeriesType { get; }

        /// <summary>
        /// Complete stream length
        /// </summary>
        int Originalsize { get; }

        /// <summary>
        /// Resolution of the stream
        /// </summary>
        StreamResolution Resolution { get; }

    }
}
