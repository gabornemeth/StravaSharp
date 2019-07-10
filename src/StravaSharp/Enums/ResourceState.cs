//
// ResourceState.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2017, Gabor Nemeth
//


namespace StravaSharp
{
    /// <summary>
    /// Indicates the representation of the returned object.
    /// </summary>
    public enum ResourceState
    {
        /// <summary>
        /// Meta representation
        /// </summary>
        Meta = 1,
        /// <summary>
        /// Summary representation
        /// </summary>
        Summary = 2,
        /// <summary>
        /// Detailed representation
        /// </summary>
        Detailed = 3
    }
}
