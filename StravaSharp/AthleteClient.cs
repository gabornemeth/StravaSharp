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

        public async Task<Athlete> Get(int id)
        {
            var request = new RestRequest(string.Format("/api/v3/athletes/{0}", id), Method.GET);
            var response = await _client.RestClient.Execute<Athlete>(request);
            return response.Data;
        }
    }
}
