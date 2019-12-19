using System.Collections.Generic;
using System.Threading.Tasks;
using DiplomaSurvive.Entities;
using DiplomaSurvive.Models;

namespace DiplomaSurvive.Services
{
    public interface IEventService
    {
        Task ClearEventLeaderboards(int eventId);

        Task FinalizeResults(int eventId);

        Task<List<EventDto>> GetOldEventsAsync();
        
        Task<EventDto> GetAsync();
    }
}