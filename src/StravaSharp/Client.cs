using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace StravaSharp
{
    /// <summary>
    /// Strava client
    /// </summary>
    public class Client
    {
        private readonly IRestClient _restClient;

        virtual internal protected IRestClient RestClient => _restClient;

        public IAuthenticator Authenticator { get; }

        public Client(IRestClient restClient)
        {
            _restClient = restClient;
            
            Athletes = new AthleteClient(this);
            Activities = new ActivityClient(this);
            Segments = new SegmentClient(this);
            SegmentEfforts = new SegmentEffortsClient(this);
            Clubs = new ClubClient(this);
        }

        public static Client Create(IAuthenticator authenticator)
        {
            var restClient = new RestClient("https://www.strava.com", options =>
            {
                options.Authenticator = authenticator;
            });

            return new Client(restClient);
        }

        public AthleteClient Athletes { get; }
        /// <summary>
        /// Activities endpoint
        /// </summary>
        public ActivityClient Activities { get; }
        /// <summary>
        /// Segments endpoint
        /// </summary>
        public SegmentClient Segments { get; }

        /// <summary>
        /// Segment efforts endpoint
        /// </summary>
        public SegmentEffortsClient SegmentEfforts { get; }

        /// <summary>
        /// Clubs endpoint
        /// </summary>
        public ClubClient Clubs { get; }

        public async Task<bool> Deauthorize(string accessToken)
        {
            var request = new RestRequest("/oauth/deauthorize", Method.Post);
            request.AddQueryParameter("access_token", accessToken);
            var response = await RestClient.ExecuteAsync(request);
            return response.Content?.Contains(accessToken) ?? false;
        }
    }
}
