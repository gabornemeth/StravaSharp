using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using RestSharp;

namespace StravaSharp
{
    public class SegmentClient
    {
        private readonly Client _client;
        private const string EndPoint = "/api/v3/segments";

        internal SegmentClient(Client client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<Segment> Get(long segmentId)
        {
            var request = new RestRequest(EndPoint + "/{id}", Method.Get);
            request.AddParameter("id", segmentId, ParameterType.UrlSegment);
            return await _client.RestClient.ExecuteForJson<Segment>(request);
        }

        public async Task<IEnumerable<SegmentSummary>> Explore(LatLng southWest, LatLng northEast, ActivityType activityType = ActivityType.Ride)
        {
            var request = new RestRequest(EndPoint + "/explore", Method.Get);
            request.AddParameter("bounds",
                string.Format("{0},{1},{2},{3}", southWest.Latitude.ToString(CultureInfo.InvariantCulture), southWest.Longitude.ToString(CultureInfo.InvariantCulture),
                northEast.Latitude.ToString(CultureInfo.InvariantCulture), northEast.Longitude.ToString(CultureInfo.InvariantCulture)));
            var segments = await _client.RestClient.ExecuteForJson<SegmentCollection>(request);
            return segments.Segments;
        }

        public Task<IEnumerable<Stream>> GetSegmentStreams(SegmentSummary segment, params StreamType[] types)
        {
            return GetSegmentStreams(segment.Id, types);
        }

        public async Task<IEnumerable<Stream>> GetSegmentStreams(long segmentId, params StreamType[] types)
        {
            var request = new RestRequest("/api/v3/segments/{id}/streams/{types}", Method.Get);
            request.AddParameter("id", segmentId, ParameterType.UrlSegment);
            request.AddParameter("types", EnumHelper.ToString<StreamType>(types), ParameterType.UrlSegment);
            return await _client.RestClient.ExecuteForJson<Stream[]>(request);
        }

        public Task<IEnumerable<Stream>> GetEffortStreams(SegmentEffort effort, params StreamType[] types)
        {
            return GetEffortStreams(effort.Id, types);
        }

        public async Task<IEnumerable<Stream>> GetEffortStreams(long segmentEffortId, params StreamType[] types)
        {
            var request = new RestRequest("/api/v3/segment_efforts/{id}/streams/{types}", Method.Get);
            request.AddParameter("id", segmentEffortId, ParameterType.UrlSegment);
            request.AddParameter("types", EnumHelper.ToString<StreamType>(types), ParameterType.UrlSegment);
            return await _client.RestClient.ExecuteForJson<Stream[]>(request);
        }
    }
}
