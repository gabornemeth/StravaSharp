using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaSharp.FakeStrava
{
    internal class FakeAthleteClient : IAthleteClient
    {
        private FakeClient fakeClient;
        internal const int cAthleteIdBase = 2000;
        internal Dictionary<int,Athlete> Athletes;
        internal int CurrentAthleteId = cAthleteIdBase;
        public FakeAthleteClient(FakeClient fakeClient)
        {
            this.fakeClient = fakeClient;
            var athletes = new Dictionary<int,Athlete>();
            for(int i=0; i<fakeClient.Config.NumAthletes;++i)
            {
                int id = cAthleteIdBase + i;
                athletes[id]=new Athlete
                {
                    AthleteType = AthleteType.Cyclist,
                    FirstName=$"First{id}",
                    LastName=$"Last{id}",
                    Id=id,
                    Sex=(i%2==1) ?  "F" : "M"
                };
            }
            Athletes = athletes;
        }

        public Task<Athlete> Get(int athleteId)
        {
            return fakeClient.MakeTask(Athletes[athleteId]);
        }

        public Task<Athlete> GetCurrent()
        {
            return fakeClient.MakeTask(Athletes[CurrentAthleteId]);
        }
    }
}