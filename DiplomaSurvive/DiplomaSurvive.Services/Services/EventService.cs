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
        private readonly IRepository<Event> _eventRepository;

        public EventService(IRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
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
    }
}