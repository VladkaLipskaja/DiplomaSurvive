using System.Threading.Tasks;
using DiplomaSurvive.Entities;

namespace DiplomaSurvive.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> _playerRepository;

        public PlayerService(IRepository<Player> playerRepository)
        {
            _playerRepository = playerRepository;
        }
        
        public async Task AddAsync(string name)
        {
            var user = await _playerRepository.GetAsync(p => p.UserName.ToLower() == name.ToLower());

            if (user != null)
            {
                //TODO: add exception
            }

            var player = new Player
            {
                UserName = name
            };

            await _playerRepository.AddAsync(player);
        }
    }
}