using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace StravaSharp
{
    public class SegmentEffortsClient
    {
        private readonly Client _client;
        private const string EndPoint = "/api/v3/segment_efforts";

        internal SegmentEffortsClient(Client client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, DateTime? startDateLocal = null, DateTime? endDateLocal = null, int? perPage = null)
        {
            var request = new RestRequest(EndPoint, Method.GET);
            request.AddParameter("segment_id", segmentId);
            if (startDateLocal != null)
                request.AddParameter("start_date_local", startDateLocal.Value.ToIso8601DateTimeString());
            if (endDateLocal != null)
                request.AddParameter("end_date_local", endDateLocal.Value.ToIso8601DateTimeString());
            if (perPage != null)
                request.AddParameter("per_page", perPage);
            var response = await _client.RestClient.Execute<SegmentEffort[]>(request);
            return response.Data;
        }
    }
}
