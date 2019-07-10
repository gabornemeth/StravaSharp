using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StravaSharp.FakeStrava
{
    internal class FakeSegmentEffortClient : ISegmentEffortClient
    {
        private FakeClient fakeClient;
        const long cSegmentEffortIdBase = 6000;
        private long nextId = cSegmentEffortIdBase;
        readonly Dictionary<long, SegmentEffort> SegmentEfforts;

        public FakeSegmentEffortClient(FakeClient fakeClient)
        {
            this.fakeClient = fakeClient;
            SegmentEfforts = new Dictionary<long, SegmentEffort>();
        }

        internal SegmentEffort Create()
        {
            long id = nextId++;
            SegmentEfforts[id] = new SegmentEffort { Id=id };
            return SegmentEfforts[id];
        }

        public Task<SegmentEffort> Get(long segmentEffortId)
        {
            return fakeClient.MakeTask(SegmentEfforts[segmentEffortId]);
        }

        public Task<IReadOnlyList<Stream>> GetEffortStreams(long segmentEffortId, params StreamType[] types)
        {
            throw new NotImplementedException("GetEffortStreams");
        }

    }
}