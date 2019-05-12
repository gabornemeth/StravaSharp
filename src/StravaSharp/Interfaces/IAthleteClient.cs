using System.Threading.Tasks;

namespace StravaSharp
{
    public interface IAthleteClient
    {
        Task<IAthlete> Get(int athleteId);
        Task<IAthlete> GetCurrent();
    }
}