using System;
using System.Linq;
using System.Threading.Tasks;
using DiplomaSurvive.Api.Leaderboard;
using DiplomaSurvive.Models;
using DiplomaSurvive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaSurvive.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private ILeaderboardService _leaderboardService;

        private ISecurityService _securityService;
        
        public LeaderboardController(ILeaderboardService leaderboardService, ISecurityService securityService)
        {
            _leaderboardService = leaderboardService;
            _securityService = securityService;
        }
        
        [HttpGet]
        public async Task<JsonResult> GetLeaderboard()
        {
            try
            {
                var id = _securityService.GetUserId(User);

                var leaderboard = await _leaderboardService.GetLeaderboardByPlayerId(id);

                var response = new GetLeaderboardResponse
                {
                    Place = leaderboard.Place,
                    Players = leaderboard.Players.Select(x => new GetLeaderboardResponse.LeaderboardPlayer
                    {
                        Name = x.Name,
                        Place = x.Place,
                        Score = x.Score
                    }).ToArray()
                };

                return this.JsonApi(response);
            }
            catch (SecurityException exception)
            {
                return this.JsonApi(exception);
            }
            catch (PlayerException exception)
            {
                return this.JsonApi(exception);
            }
            catch (EventException exception)
            {
                return this.JsonApi(exception);
            }
        }
    }
}