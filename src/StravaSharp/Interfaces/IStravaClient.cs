using RestSharp.Portable;

namespace StravaSharp
{
    public interface IStravaClient
    {
        IActivityClient Activities { get; }
        IAthleteClient Athletes { get; }
        IAuthenticator Authenticator { get; }
        IClubClient Clubs { get; }
        ISegmentClient Segments { get; }
    }
}