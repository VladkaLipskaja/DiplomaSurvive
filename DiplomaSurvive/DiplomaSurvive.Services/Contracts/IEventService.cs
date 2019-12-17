using System.Collections.Generic;
using System.Threading.Tasks;
using DiplomaSurvive.Models;

namespace DiplomaSurvive.Services
{
    public interface IEventService
    {
        Task<EventDto> GetAsync();
    }
}