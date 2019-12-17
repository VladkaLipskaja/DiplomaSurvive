using System.Linq;
using System.Threading.Tasks;
using DiplomaSurvive.Entities;
using DiplomaSurvive.Models;

namespace DiplomaSurvive.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> _playerRepository;

        private readonly ISecurityService _securityService;

        public PlayerService(IRepository<Player> playerRepository, ISecurityService securityService)
        {
            _playerRepository = playerRepository;
            _securityService = securityService;
        }
        
        public async Task AddAsync(string name)
        {
            var user = await _playerRepository.GetAsync(p => p.UserName.ToLower() == name.ToLower());

            if (user != null)
            {
                throw new PlayerException(PlayerErrorCode.AlreadyExists);
            }

            var player = new Player
            {
                UserName = name
            };

            await _playerRepository.AddAsync(player);
        }

        public async Task<int> SetUserScoresAsync(int id, int scores)
        {
            var player = (await _playerRepository.GetAsync(p => p.ID == id)).FirstOrDefault();

            if (player == null)
            {
                throw new PlayerException(PlayerErrorCode.UnrecognizedUser);
            }
            
            if (player.Scores < scores)
            {
                player.Scores = scores;
            }

            await _playerRepository.UpdateAsync(player);

            return player.Scores;
        }

        /// <summary>
        /// Authenticates the user asynchronously.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// The token.
        /// </returns>
        /// <exception cref="UserException">
        /// Invalid credentials.
        /// </exception>
        public async Task<PlayerDto> AuthenticateUserAsync(string name)
        {
            var user = (await _playerRepository.GetAsync(p => p.UserName.ToLower() == name.ToLower())).FirstOrDefault();

            if (user == null)
            {
                throw new PlayerException(PlayerErrorCode.UnrecognizedUser);
            }

            var token = _securityService.GetToken(user.ID);

            var authPlayer = new PlayerDto
            {
                Token = token,
                Reward = user.Reward,
                Scores = user.Scores
            };

            await UpdatePlayerScores(user);

            return authPlayer;
        }

        private async Task UpdatePlayerScores(Player player)
        {
            player.Scores += player.Reward;
            player.Reward = 0;

            await _playerRepository.UpdateAsync(player);
        }
    }
}