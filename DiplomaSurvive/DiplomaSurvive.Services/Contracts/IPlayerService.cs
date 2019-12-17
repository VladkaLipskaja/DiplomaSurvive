using System.Threading.Tasks;
using DiplomaSurvive.Models;

namespace DiplomaSurvive.Services
{
    public interface IPlayerService
    {
        Task AddAsync(string name);

        Task<PlayerDto> AuthenticateUserAsync(string name);

        Task<int> SetUserScoresAsync(int id, int scores);
    }
}