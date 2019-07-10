using System.Collections.Generic;
using System.Threading.Tasks;

namespace StravaSharp
{
    public interface IClubClient
    {
        Task<Club> Get(int id);
        Task<IReadOnlyList<AthleteSummary>> GetAdmins(int id);
        Task<IReadOnlyList<AthleteSummary>> GetAdmins(int clubId, int page, int itemsPerPage);
        Task<IReadOnlyList<AthleteSummary>> GetMembers(int id);
        Task<IReadOnlyList<AthleteSummary>> GetMembers(int id, int page, int itemsPerPage);
    }
}