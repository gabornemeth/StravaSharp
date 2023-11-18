//
// AthleteClient.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2018, Gabor Nemeth
//

using System.Threading.Tasks;
using RestSharp;

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
            var request = new RestRequest("/api/v3/athlete", Method.Get);
            return await _client.RestClient.ExecuteForJson<Athlete>(request);
        }

        public async Task<Athlete> Get(long athleteId)
        {
            var request = new RestRequest(string.Format("/api/v3/athletes/{0}", athleteId), Method.Get);
            return await _client.RestClient.ExecuteForJson<Athlete>(request);
        }
    }
}
