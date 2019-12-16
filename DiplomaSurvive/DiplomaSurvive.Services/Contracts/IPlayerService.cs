using System.Threading.Tasks;

namespace DiplomaSurvive.Services
{
    public interface IPlayerService
    {
        Task AddAsync(string name);
    }
}