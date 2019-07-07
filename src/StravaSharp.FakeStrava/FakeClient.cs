using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp.Portable;
using System.Linq;

namespace StravaSharp.FakeStrava
{
    public class FakeClientConfig
    {
        public int NumAthletes { get; } = 10;
        public int NumActivitiesPerAthlete { get; } = 100;
        public double FractionManualActivies { get; } = 0.1;
        public int SegmentsPerActivity { get; } = 10;
        public int SegmentsOverall { get; } = 100;
        public int CallDelayMillisecs { get; } = 100;
        public int NumClubs { get; } = 2;
        public int ActivitiesPerDayPerAthlete { get; } = 1;
        public float MinAthleteSpeedKph { get; } = 20;
        public float MaxAthleteSpeedKph { get; } = 45;
        public DateTime MostRecentActivity { get; } = DateTime.Today;
        public LatLng SegmentCentre { get; } = new LatLng { Latitude = 52.2F, Longitude = 0.12F };
        public string SegmentCity { get; } = "Cambridge";
        public string SegmentCountry { get; } = "United Kingdom";
        public float SegmentLengthMetres { get; } = 1000;

        public float ActivityLengthMetres => SegmentLengthMetres * SegmentsPerActivity;
    }




    public class FakeClient : IStravaClient
    {
        public FakeClientConfig Config { get; }
        /// <summary>
        /// Construct a fake Strava client supporting IStravaClient
        /// </summary>
        /// <param name="config">Parameters specifying what is to be constructed</param>
        /// <param name="authenticator">Not used, just returned by IStravaClient.Authenticator.  May be null</param>
        public FakeClient(FakeClientConfig config, IAuthenticator authenticator)
        {
            Config = config;
            Authenticator = authenticator;
            // The order of these is important; each relies on predecessors
            var clubs = new FakeClubClient(this);
            Clubs = clubs;
            var athletes = new FakeAthleteClient(this);
            Athletes = athletes;
            clubs.PopulateClubs(athletes.Athletes.Values);
            SegmentEfforts = new FakeSegmentEffortClient(this);
            Segments = new FakeSegmentClient(this);
            Activities = new FakeActivityClient(this);
        }
        public IAuthenticator Authenticator { get; }

        internal FakeActivityClient Activities { get; }

        internal FakeAthleteClient Athletes { get; }

        internal FakeClubClient Clubs { get; }

        internal FakeSegmentClient Segments { get; }

        internal FakeSegmentEffortClient SegmentEfforts { get; }

        IActivityClient IStravaClient.Activities => Activities;

        IAthleteClient IStravaClient.Athletes => Athletes;

        IClubClient IStravaClient.Clubs => Clubs;

        ISegmentClient IStravaClient.Segments => Segments;
        ISegmentEffortClient IStravaClient.SegmentEfforts => SegmentEfforts;

        internal async Task<T> MakeTask<T>(T result)
        {
            await Task.Delay(Config.CallDelayMillisecs);
            return result;
        }
        internal async Task<IReadOnlyList<T>> Paginate<T>(Task<IReadOnlyList<T>> data, int page, int itemsPerPage)
        {
            IReadOnlyList<T> pageData = (await data).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            return pageData;
        }

    }
}
