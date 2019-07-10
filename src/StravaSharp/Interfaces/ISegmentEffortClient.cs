using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StravaSharp
{
    public interface ISegmentEffortClient
    {
        Task<SegmentEffort> Get(long segmentId);
        Task<IReadOnlyList<Stream>> GetEffortStreams(long segmentEffortId, params StreamType[] types);
    }
}