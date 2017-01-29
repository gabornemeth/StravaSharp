using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StravaSharp
{
    /// <summary>
    /// Club endpoint
    /// </summary>
    public class ClubClient
    {
        private Client _client;
        private const string EndPoint = "/api/v3/clubs";

        internal ClubClient(Client client)
        {
            _client = client;
        }

        public async Task<Club> Get(int id)
        {
            var request = new RestRequest(EndPoint + "/{id}", Method.GET);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<Club>(request);
            return response.Data;
        }

        public Task<List<AthleteSummary>> GetMembers(int id)
        {
            return GetMembers(id, 0, 0);
        }

        public async Task<List<AthleteSummary>> GetMembers(int id, int page, int itemsPerPage)
        {
            var request = new RestRequest(EndPoint + "/{id}/members", Method.GET);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            var response = await _client.RestClient.Execute<List<AthleteSummary>>(request);
            return response.Data;
        }

        /// <summary>
        /// Retrieve summary information about admins of a specific club, with the owner on top and sorted by first names.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<AthleteSummary>> GetAdmins(int id)
        {
            return GetAdmins(id, 0, 0);
        }

		/// <summary>
		/// Retrieve the admins of the club
		/// </summary>
		/// <returns>The admins.</returns>
		/// <param name="clubId">Identifier of the club.</param>
		/// <param name="page">Page.</param>
		/// <param name="itemsPerPage">Items per page.</param>
		public async Task<List<AthleteSummary>> GetAdmins(int clubId, int page, int itemsPerPage)
        {
            var request = new RestRequest(EndPoint + "/{id}/admins", Method.GET);
            request.AddParameter("id", clubId, ParameterType.UrlSegment);
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            //var response = await _client.RestClient.Execute(request);
            //response.d
            var response = await _client.RestClient.Execute<List<AthleteSummary>>(request);
            return response.Data;
        }
    }
}
