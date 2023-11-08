using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using RestSharp.Portable;

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
            var request = new RestRequest(EndPoint + "/{id}", Method.GET);
            request.AddParameter("id", segmentId, ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<Segment>(request);
            return response.Data;
        }

        public async Task<List<SegmentSummary>> Explore(LatLng southWest, LatLng northEast, ActivityType activityType = ActivityType.Ride)
        {
            var request = new RestRequest(EndPoint + "/explore", Method.GET);
            request.AddParameter("bounds",
                string.Format("{0},{1},{2},{3}", southWest.Latitude.ToString(CultureInfo.InvariantCulture), southWest.Longitude.ToString(CultureInfo.InvariantCulture),
                northEast.Latitude.ToString(CultureInfo.InvariantCulture), northEast.Longitude.ToString(CultureInfo.InvariantCulture)));
            var response = await _client.RestClient.Execute<SegmentCollection>(request);
            return response.Data.Segments;
        }

        public Task<List<Stream>> GetSegmentStreams(SegmentSummary segment, params StreamType[] types)
        {
            return GetSegmentStreams(segment.Id, types);
        }

        public async Task<List<Stream>> GetSegmentStreams(long segmentId, params StreamType[] types)
        {
            var request = new RestRequest("/api/v3/segments/{id}/streams/{types}", Method.GET);
            request.AddParameter("id", segmentId, ParameterType.UrlSegment);
            request.AddParameter("types", EnumHelper.ToString<StreamType>(types), ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<List<Stream>>(request);
            return response.Data;
        }

        public Task<List<Stream>> GetEffortStreams(SegmentEffort effort, params StreamType[] types)
        {
            return GetEffortStreams(effort.Id, types);
        }

        public async Task<List<Stream>> GetEffortStreams(long segmentEffortId, params StreamType[] types)
        {
            var request = new RestRequest("/api/v3/segment_efforts/{id}/streams/{types}", Method.GET);
            request.AddParameter("id", segmentEffortId, ParameterType.UrlSegment);
            request.AddParameter("types", EnumHelper.ToString<StreamType>(types), ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<List<Stream>>(request);
            return response.Data;
        }

    }
}
