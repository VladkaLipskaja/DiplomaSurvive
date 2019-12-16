using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomaSurvive.Entities;
using DiplomaSurvive.Models;

namespace DiplomaSurvive.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IRepository<Leaderboard> _leaderboardRepository;
        private readonly IRepository<Player> _playerRepository;

        public LeaderboardService(IRepository<Leaderboard> leaderboardRepository, IRepository<Player> playerRepository)
        {
            _leaderboardRepository = leaderboardRepository;
            _playerRepository = playerRepository;
        }

        public async Task<LeaderboardDto> GetLeaderboardByPlayerId(int id)
        {
            var player = (await _playerRepository.GetAsync(p => p.ID == id)).FirstOrDefault();

            if (player == null)
            {
                // TODO: throw exception
            }

            if (player.LeaderboardID == null)
            {
                // TODO: throw exception
            }

            var users =
                (await _playerRepository.GetAsync(p => p.LeaderboardID == player.LeaderboardID)).ToList();

            users = users.OrderBy(x => x.Scores).ToList();

            var leaderboardPlayers = new List<LeaderboardDto.LeaderboardPlayer>();

            var previousScore = -1;
            var currentPlace = 0;
            var placeOfCurrentPlayer = 0;

            // TODO: move it to another method
            
            for (int i = 0; i < users.Count; i++)
            {
                if (previousScore != users[i].Scores)
                {
                    currentPlace++;
                }

                if (users[i].ID == id)
                {
                    placeOfCurrentPlayer = currentPlace;
                }

                leaderboardPlayers.Add(new LeaderboardDto.LeaderboardPlayer
                {
                    Name = users[i].UserName,
                    Place = currentPlace,
                    Score = users[i].Scores
                });
            }

            return new LeaderboardDto
            {
                Place = placeOfCurrentPlayer,
                Players = leaderboardPlayers.ToArray()
            };
        }
    }
}