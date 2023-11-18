using RestSharp;
using System.Collections.Generic;
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
            var request = new RestRequest(EndPoint + "/{id}", Method.Get);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            return await _client.RestClient.ExecuteForJson<Club>(request);
        }

        public Task<IEnumerable<AthleteSummary>> GetMembers(int id)
        {
            return GetMembers(id, 0, 0);
        }

        public async Task<IEnumerable<AthleteSummary>> GetMembers(int id, int page, int itemsPerPage)
        {
            var request = new RestRequest(EndPoint + "/{id}/members", Method.Get);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            return await _client.RestClient.ExecuteForJson<List<AthleteSummary>>(request);
        }

        /// <summary>
        /// Retrieve summary information about admins of a specific club, with the owner on top and sorted by first names.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<IEnumerable<AthleteSummary>> GetAdmins(int id)
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
		public async Task<IEnumerable<AthleteSummary>> GetAdmins(int clubId, int page, int itemsPerPage)
        {
            var request = new RestRequest(EndPoint + "/{id}/admins", Method.Get);
            request.AddParameter("id", clubId, ParameterType.UrlSegment);
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            return await _client.RestClient.ExecuteForJson<List<AthleteSummary>>(request);
        }
    }
}
