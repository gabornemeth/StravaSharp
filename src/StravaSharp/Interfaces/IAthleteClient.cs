using System.Threading.Tasks;

namespace StravaSharp
{
    public interface IAthleteClient
    {
        Task<Athlete> Get(int athleteId);
        Task<Athlete> GetCurrent();
    }
}