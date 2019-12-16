using System.Threading.Tasks;
using DiplomaSurvive.Models;

namespace DiplomaSurvive.Services
{
    public interface ILeaderboardService
    {
        Task<LeaderboardDto> GetLeaderboardByPlayerId(int id);
    }
}