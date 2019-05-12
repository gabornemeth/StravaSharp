using System.Collections.Generic;
using System.Threading.Tasks;

namespace StravaSharp
{
    public interface IClubClient
    {
        Task<IClub> Get(int id);
        Task<IReadOnlyList<IAthleteSummary>> GetAdmins(int id);
        Task<IReadOnlyList<IAthleteSummary>> GetAdmins(int clubId, int page, int itemsPerPage);
        Task<IReadOnlyList<IAthleteSummary>> GetMembers(int id);
        Task<IReadOnlyList<IAthleteSummary>> GetMembers(int id, int page, int itemsPerPage);
    }
}