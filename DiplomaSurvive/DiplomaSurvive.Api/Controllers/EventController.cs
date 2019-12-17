using System.Threading.Tasks;
using DiplomaSurvive.Models;
using DiplomaSurvive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaSurvive.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<JsonResult> GetEvent()
        {
            try
            {
                var evn = await _eventService.GetAsync();
                return this.JsonApi(evn);
            }
            catch (EventException exception)
            {
                return this.JsonApi(exception);
            }
        }
    }
}