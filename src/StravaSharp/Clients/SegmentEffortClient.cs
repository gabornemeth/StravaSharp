using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace StravaSharp
{
    public class SegmentEffortClient : ISegmentEffortClient
    {
        private StravaClient _client;
        private const string EndPoint = "/api/v3/segment_efforts";

        internal SegmentEffortClient(StravaClient client)
        {
            _client = client;
        }

        public async Task<SegmentEffort> Get(long segmentEffortId)
        {
            var request = new RestRequest(EndPoint + "/{id}", Method.GET);
            request.AddParameter("id", segmentEffortId, ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<SegmentEffort>(request);
            return response.Data;
        }

        public async Task<IReadOnlyList<Stream>> GetEffortStreams(long segmentEffortId, params StreamType[] types)
        {
            var request = new RestRequest("/api/v3/segment_efforts/{id}/streams/{types}", Method.GET);
            request.AddParameter("id", segmentEffortId, ParameterType.UrlSegment);
            request.AddParameter("types", EnumHelper.ToString<StreamType>(types), ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<List<Stream>>(request);
            return response.Data;
        }

    }
}
