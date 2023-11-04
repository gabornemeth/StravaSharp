﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace StravaSharp
{
    public class SegmentClient
    {
        private Client _client;
        private const string EndPoint = "/api/v3/segments";

        internal SegmentClient(Client client)
        {
            _client = client;
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

        public Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int page, int perPage)
        {
            return GetEfforts(segmentId, null, null, null, page, perPage);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId)
        {
            return GetEfforts(segmentId, null, null, null, null, null);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int athleteId)
        {
            return GetEfforts(segmentId, athleteId, null, null, null, null);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int athleteId, int page, int perPage)
        {
            return GetEfforts(segmentId, athleteId, null, null, page, perPage);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, DateTime startDateLocal, DateTime endDateLocal)
        {
            return GetEfforts(segmentId, null, startDateLocal, endDateLocal, null, null);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, DateTime startDateLocal, DateTime endDateLocal, int page, int perPage)
        {
            return GetEfforts(segmentId, null, startDateLocal, endDateLocal, page, perPage);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int athleteId, DateTime startDateLocal, DateTime endDateLocal)
        {
            return GetEfforts(segmentId, athleteId, startDateLocal, endDateLocal, null, null);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int athleteId, DateTime startDateLocal, DateTime endDateLocal, int page, int perPage)
        {
            return GetEfforts(segmentId, athleteId, startDateLocal, endDateLocal, page, perPage);
        }

        private async Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int? athleteId, DateTime? startDateLocal, DateTime? endDateLocal, int? page, int? perPage)
        {
            var request = new RestRequest(EndPoint + "/" + segmentId + "/all_efforts", Method.GET);
            if (athleteId != null)
                request.AddParameter("athlete_id", athleteId.Value);
            if (startDateLocal != null)
                request.AddParameter("start_date_local", startDateLocal);
            if (endDateLocal != null)
                request.AddParameter("end_date_local", endDateLocal);
            if (page != null)
                request.AddParameter("page", page);
            if (perPage != null)
                request.AddParameter("per_page", perPage);
            var response = await _client.RestClient.Execute<SegmentEffort[]>(request);
            return response.Data;
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
