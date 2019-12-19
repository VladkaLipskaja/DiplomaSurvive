using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiplomaSurvive.Entities;
using DiplomaSurvive.Models;

namespace DiplomaSurvive.Services
{
    public class EventService : IEventService
    {
        private readonly int ParallelTasks = 10;

        private readonly IRepository<Event> _eventRepository;

        private readonly IRepository<Leaderboard> _leaderboardRepository;

        private readonly IRepository<Player> _playerRepository;

        public EventService(IRepository<Event> eventRepository, IRepository<Leaderboard> leaderboardRepository,
            IRepository<Player> playerRepository)
        {
            _eventRepository = eventRepository;
            _leaderboardRepository = leaderboardRepository;
            _playerRepository = playerRepository;
        }

        public async Task<EventDto> GetAsync()
        {
            var month = DateTime.UtcNow.AddMonths(-1).ToUnixTime();
            var now = DateTime.UtcNow.ToUnixTime();
            var evn = (await _eventRepository.GetAsync(e => e.Start < now && e.Start > month && e.Finish > now))
                .FirstOrDefault();

            if (evn == null)
            {
                throw new EventException(EventErrorCode.NoEvents);
            }

            return new EventDto
            {
                ID = evn.ID,
                Finish = evn.Finish,
                Start = evn.Start,
                Title = evn.Title
            };
        }

        public async Task<List<EventDto>> GetOldEventsAsync()
        {
            var month = DateTime.UtcNow.AddMonths(-1).ToUnixTime();
            var oldEvnts = (await _eventRepository.GetAsync(e => e.Finish < month)).ToList();
            var evnts = new List<EventDto>();

            foreach (var e in oldEvnts)
            {
                evnts.Add(new EventDto
                {
                    ID = e.ID,
                    Finish = e.Finish,
                    Start = e.Start,
                    Title = e.Title
                });
            }

            return evnts;
        }

        public async Task ClearEventLeaderboards(int eventId)
        {
            var evn = (await _eventRepository.GetAsync(x => x.ID == eventId)).FirstOrDefault();

            if (evn != null)
            {
                var leaderboards = (await _leaderboardRepository.GetAsync(x => x.EventID == eventId)).ToArray();

                Parallel.ForEach(leaderboards, (l) => { l.Players = null; });

                await _leaderboardRepository.UpdateRangeAsync(leaderboards);

                await _eventRepository.DeleteAsync(evn);
            }
        }

        public async Task FinalizeResults(int eventId)
        {
            var evn = (await _eventRepository.GetAsync(x => x.ID == eventId)).FirstOrDefault();

            if (evn != null)
            {
                var leaderboards = (await _leaderboardRepository.GetAsync(x => x.EventID == eventId)).ToArray();

                Parallel.ForEach(leaderboards, async (l) => { await AwardWinnersAsync(l, evn); });
            }
        }

        private async Task AwardWinnersAsync(Leaderboard l, Event evn)
        {
            var users = l.Players.OrderByDescending(x => x.Scores).Take(3).ToArray(); // Just because of three rewards

            users[0].Reward = evn.Reward1;

            if (users.Length > 1)
            {
                users[1].Reward = evn.Reward2;

                if (users.Length == 3)
                {
                    users[2].Reward = evn.Reward3;
                }
            }

            await _playerRepository.UpdateRangeAsync(users);
        }
    }
}