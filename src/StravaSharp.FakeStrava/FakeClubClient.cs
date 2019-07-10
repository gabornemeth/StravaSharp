using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaSharp.FakeStrava
{
    internal class FakeClubClient : IClubClient
    {
        private FakeClient fakeClient;
        private Dictionary<int, Club> clubs;
        private Dictionary<int, List<Athlete>> admins;
        private Dictionary<int, List<Athlete>> members;
        const int cClubIdBase = 1000;

        public FakeClubClient(FakeClient fakeClient)
        {
            this.fakeClient = fakeClient;
            clubs = new Dictionary<int, Club>(fakeClient.Config.NumClubs);
            admins = new Dictionary<int, List<Athlete>>(fakeClient.Config.NumClubs);
            members = new Dictionary<int, List<Athlete>>(fakeClient.Config.NumClubs);
            for (int i=0; i<fakeClient.Config.NumClubs;++i)
            {
                int id = cClubIdBase + i;
                clubs[id] = new Club {
                    Country = "UK",
                    Name = $"Club{id}",
                    Id=id
                };
                admins[i] = new List<Athlete>();
                members[i] = new List<Athlete>();
            }
        }

        internal void PopulateClubs(IEnumerable<Athlete> allAthletes)
        {
            int clubCount = clubs.Count;
            int i = 0;
            foreach (Athlete athlete in allAthletes)
            {
                int clubId = i % (clubCount + 1);
                if (clubId<clubCount)   // clubId==clubCount => not a club member
                {
                    members[clubId].Add(athlete);
                    if (admins[clubId].Count == 0)
                    {
                        admins[clubId].Add(athlete);
                    }
                }
                ++i;
            }
        }

        public Task<Club> Get(int id)
        {
            return fakeClient.MakeTask(clubs[id]);
        }

        public Task<IReadOnlyList<AthleteSummary>> GetAdmins(int id)
        {
            IReadOnlyList<AthleteSummary> result = admins[id].Select(a => (AthleteSummary)a).ToList();
            return fakeClient.MakeTask(result);
        }

        public Task<IReadOnlyList<AthleteSummary>> GetAdmins(int clubId, int page, int itemsPerPage)
        {
            return fakeClient.Paginate(GetAdmins(clubId), page, itemsPerPage);
        }


        public Task<IReadOnlyList<AthleteSummary>> GetMembers(int id)
        {
            IReadOnlyList<AthleteSummary> result = members[id].Select(a => (AthleteSummary)a).ToList();
            return fakeClient.MakeTask(result);
        }

        public Task<IReadOnlyList<AthleteSummary>> GetMembers(int id, int page, int itemsPerPage)
        {
            return fakeClient.Paginate(GetMembers(id), page, itemsPerPage);
        }
    }
}