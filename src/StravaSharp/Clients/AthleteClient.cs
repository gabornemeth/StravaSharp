//
// AthleteClient.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2018, Gabor Nemeth
//

using System.Threading.Tasks;
using RestSharp.Portable;

namespace StravaSharp
{
    /// <summary>
    /// Athlete functionality
    /// </summary>
    internal class AthleteClient : IAthleteClient
    {
        private StravaClient _client;

        internal AthleteClient(StravaClient client)
        {
            _client = client;
        }

        public async Task<IAthlete> GetCurrent()
        {
            var request = new RestRequest("/api/v3/athlete", Method.GET);
            var response = await _client.RestClient.Execute<Athlete>(request);
            return response.Data;
        }

        public async Task<IAthlete> Get(int athleteId)
        {
            var request = new RestRequest(string.Format("/api/v3/athletes/{0}", athleteId), Method.GET);
            var response = await _client.RestClient.Execute<Athlete>(request);
            return response.Data;
        }
    }
}
