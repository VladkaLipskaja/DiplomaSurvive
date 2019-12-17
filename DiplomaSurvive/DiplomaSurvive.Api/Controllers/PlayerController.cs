using System;
using System.Net;
using System.Threading.Tasks;
using DiplomaSurvive.Models;
using DiplomaSurvive.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaSurvive.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private IPlayerService _playerService;

        private ISecurityService _securityService;

        public PlayerController(IPlayerService playerService, ISecurityService securityService)
        {
            _playerService = playerService;
            _securityService = securityService;
        }

        [HttpPost("authentication")]
        public async Task<JsonResult> AuthenticateUser([FromBody] AuthenticateUserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"Request {nameof(request)} should be not null.");
            }

            try
            {
                var player = await _playerService.AuthenticateUserAsync(request.Name);

                var response = new AuthenticateUserResponse
                {
                    Token = player.Token,
                    Reward = player.Reward,
                    Scores = player.Scores
                };

                return this.JsonApi(response);
            }
            catch (PlayerException exception)
            {
                return this.JsonApi(exception);
            }
        }
        
        [HttpPost("sign-in")]
        public async Task<JsonResult> SignInUser([FromBody] SignInUserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"Request {nameof(request)} should be not null.");
            }

            try
            {
                await _playerService.AddAsync(request.Name);

                var player = await _playerService.AuthenticateUserAsync(request.Name);
                
                var response = new SignInUserResponse
                {
                    Token = player.Token,
                    Reward = player.Reward,
                    Scores = player.Scores
                };

                return this.JsonApi(response);
            }
            catch (PlayerException exception)
            {
                return this.JsonApi(exception);
            }
        }
        
        [HttpPut("scores")]
        public async Task<JsonResult> SetUserScores([FromBody] SetUserScoresRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"Request {nameof(request)} should be not null.");
            }

            try
            {
                var id = _securityService.GetUserId(User);
                
                var scores = await _playerService.SetUserScoresAsync(id, request.Scores);
                
                var response = new SetUserScoresResponse
                {
                    Scores = scores
                };

                return this.JsonApi(response);
            }
            catch (PlayerException exception)
            {
                return this.JsonApi(exception);
            }
            catch (SecurityException exception)
            {
                return this.JsonApi(exception);
            }
        }
    }
}