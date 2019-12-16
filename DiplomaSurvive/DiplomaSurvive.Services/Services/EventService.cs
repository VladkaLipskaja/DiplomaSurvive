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
        
        public async Task<List<EventDto>> GetAsync()
        {
            var events = (await _eventRepository.ListAllAsync()).Select(e => new EventDto
            {
                ID = e.ID,
                Finish = e.Finish,
                Start = e.Start,
                Title = e.Title
            }).ToList();

            return events;
        }
    }
}