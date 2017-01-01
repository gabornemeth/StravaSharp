using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace StravaSharp
{
    /// <summary>
    /// Athlete functionality
    /// </summary>
    public class AthleteClient
    {
        private Client _client;

        internal AthleteClient(Client client)
        {
            _client = client;
        }

        public async Task<Athlete> GetCurrent()
        {
            var request = new RestRequest("/api/v3/athlete", Method.GET);
            var response = await _client.RestClient.Execute<Athlete>(request);
            return response.Data;
        }

        public async Task<Athlete> Get(int athleteId)
        {
            var request = new RestRequest(string.Format("/api/v3/athletes/{0}", athleteId), Method.GET);
            var response = await _client.RestClient.Execute<Athlete>(request);
            return response.Data;
        }

        /// <summary>
        /// Getting the friends of current athlete
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Athlete>> GetFriendsOfCurrent(int page = 0, int itemsPerPage = 0)
        {
            var request = new RestRequest("/api/v3/athlete/friends", Method.GET);
            request.AddPaging(page, itemsPerPage);
            var response = await _client.RestClient.Execute<Athlete[]>(request);
            return response.Data;
        }

        public async Task<IEnumerable<Athlete>> GetFriends(int athleteId, int page = 0, int itemsPerPage = 0)
        {
            var request = new RestRequest("/api/v3/athletes/{id}/friends", Method.GET);
            request.AddParameter("id", athleteId, ParameterType.UrlSegment);
            request.AddPaging(page, itemsPerPage);
            var response = await _client.RestClient.Execute<Athlete[]>(request);
            return response.Data;
        }

        public async Task<IEnumerable<Athlete>> GetFollowersOfCurrent(int page = 0, int itemsPerPage = 0)
        {
            var request = new RestRequest("/api/v3/athlete/followers", Method.GET);
            request.AddPaging(page, itemsPerPage);
            var response = await _client.RestClient.Execute<Athlete[]>(request);
            return response.Data;
        }

        public async Task<IEnumerable<Athlete>> GetFollowers(int athleteId, int page = 0, int itemsPerPage = 0)
        {
            var request = new RestRequest("/api/v3/athletes/{id}/followers", Method.GET);
            request.AddParameter("id", athleteId, ParameterType.UrlSegment);
            request.AddPaging(page, itemsPerPage);
            var response = await _client.RestClient.Execute<Athlete[]>(request);
            return response.Data;
        }

        /// <summary>
        /// Retrieve the athletes who both the authenticated user and the indicated athlete are following.
        /// </summary>
        /// <param name="athleteId"></param>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Athlete>> GetFriendsOfBoth(int athleteId, int page = 0, int itemsPerPage = 0)
        {
            var request = new RestRequest("/api/v3/athletes/{id}/both-following", Method.GET);
            request.AddParameter("id", athleteId, ParameterType.UrlSegment);
            request.AddPaging(page, itemsPerPage);
            var response = await _client.RestClient.Execute<Athlete[]>(request);
            return response.Data;
        }
    }
}
