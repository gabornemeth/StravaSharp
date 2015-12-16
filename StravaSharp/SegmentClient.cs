using System;
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

        public async Task<Segment> Get(int segmentId)
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

        public Task<IEnumerable<SegmentEffort>> GetEfforts(int segmentId, int page, int perPage)
        {
            return GetEfforts(segmentId, null, null, null, page, perPage);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(int segmentId)
        {
            return GetEfforts(segmentId, null, null, null, null, null);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(int segmentId, int athleteId)
        {
            return GetEfforts(segmentId, athleteId, null, null, null, null);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(int segmentId, int athleteId, int page, int perPage)
        {
            return GetEfforts(segmentId, athleteId, null, null, page, perPage);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(int segmentId, DateTime startDateLocal, DateTime endDateLocal)
        {
            return GetEfforts(segmentId, null, startDateLocal, endDateLocal, null, null);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(int segmentId, DateTime startDateLocal, DateTime endDateLocal, int page, int perPage)
        {
            return GetEfforts(segmentId, null, startDateLocal, endDateLocal, page, perPage);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(int segmentId, int athleteId, DateTime startDateLocal, DateTime endDateLocal)
        {
            return GetEfforts(segmentId, athleteId, startDateLocal, endDateLocal, null, null);
        }

        public Task<IEnumerable<SegmentEffort>> GetEfforts(int segmentId, int athleteId, DateTime startDateLocal, DateTime endDateLocal, int page, int perPage)
        {
            return GetEfforts(segmentId, athleteId, startDateLocal, endDateLocal, page, perPage);
        }

        private async Task<IEnumerable<SegmentEffort>> GetEfforts(int segmentId, int? athleteId, DateTime? startDateLocal, DateTime? endDateLocal, int? page, int? perPage)
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

        /// <summary>
        /// Gets leaderboard for a segment.
        /// </summary>
        /// <param name="segmentId">ID of the segment.</param>
        /// <param name="gender"></param>
        /// <returns>Leaderboard of the segment</returns>
        public Task<Leaderboard> GetLeaderboard(int segmentId, Gender? gender, AgeGroup? ageGroup)
        {
            return GetLeaderboardInternal(segmentId, null, null, gender, ageGroup);
        }

        public Task<Leaderboard> GetLeaderboard(int segmentId, int page, int perPage, Gender? gender, AgeGroup? ageGroup)
        {
            return GetLeaderboardInternal(segmentId, page, perPage, gender, ageGroup);
        }

        private async Task<Leaderboard> GetLeaderboardInternal(int segmentId, int? page, int? perPage, Gender? gender, AgeGroup? ageGroup, bool following = false)
        {
            var request = new RestRequest(EndPoint + "/" + segmentId + "/leaderboard", Method.GET);
            if (page != null)
                request.AddParameter("page", page);
            if (perPage != null)
                request.AddParameter("per_page", perPage);
            request.AddParameter("following", following ? "true" : "false");
            if (gender.HasValue)
                request.AddParameter("gender", gender.Value.ToStravaString());
            if (ageGroup.HasValue)
                request.AddParameter("age_group", ageGroup.Value.ToStravaString());
            var response = await _client.RestClient.Execute<Leaderboard>(request);
            return response.Data;
        }
    }
}
