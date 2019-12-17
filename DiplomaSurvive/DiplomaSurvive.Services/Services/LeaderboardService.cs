using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomaSurvive.Entities;
using DiplomaSurvive.Models;

namespace DiplomaSurvive.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly int defaultNumberOfPlaces = 30;
        
        private readonly IRepository<Leaderboard> _leaderboardRepository;
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<Event> _eventRepository;

        public LeaderboardService(IRepository<Leaderboard> leaderboardRepository, IRepository<Player> playerRepository, IRepository<Event> eventRepository)
        {
            _leaderboardRepository = leaderboardRepository;
            _playerRepository = playerRepository;
            _eventRepository = eventRepository;
        }

        public async Task<LeaderboardDto> GetLeaderboardByPlayerId(int id)
        {
            var player = (await _playerRepository.GetAsync(p => p.ID == id)).FirstOrDefault();

            if (player == null)
            {
                throw new PlayerException(PlayerErrorCode.UnrecognizedUser);
            }

            if (player.Leaderboard == null)
            {
                player.LeaderboardID = await GetDefinedLeaderboard();
                await _playerRepository.UpdateAsync(player);
            }

            var users =
                (await _playerRepository.GetAsync(p => p.LeaderboardID == player.LeaderboardID)).ToList();

            return SortLeaderboardPlayers(users, id);
        }

        private LeaderboardDto SortLeaderboardPlayers(List<Player> users, int id)
        {
            var leaderboardPlayers = new List<LeaderboardDto.LeaderboardPlayer>();

            var previousScore = -1;
            var currentPlace = 0;
            var placeOfCurrentPlayer = 0;
            
            users = users.OrderBy(x => x.Scores).ToList();

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

        private async Task<int> GetDefinedLeaderboard()
        {
            var month = DateTime.UtcNow.AddMonths(-1).ToUnixTime();
            var now = DateTime.UtcNow.ToUnixTime();
            var evn = (await _eventRepository.GetAsync(e => e.Start < now && e.Start > month && e.Finish > now))
                .FirstOrDefault();

            if (evn == null)
            {
                throw new EventException(EventErrorCode.NoEvents);
            }

            var leaderboard = (await _leaderboardRepository.GetAsync(l => l.Places - l.ReservedPlaces > 0))
                .FirstOrDefault();

            if (leaderboard == null)
            {
                leaderboard = new Leaderboard
                {
                    Places = defaultNumberOfPlaces,
                    EventID = evn.ID
                };

                await _leaderboardRepository.AddAsync(leaderboard);
            }

            return leaderboard.ID;
        }

    }
}