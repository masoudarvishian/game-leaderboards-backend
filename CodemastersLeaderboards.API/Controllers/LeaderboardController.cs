using CodemastersLeaderboards.Application.Services;
using CodemastersLeaderboards.Domain.Models.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodemastersLeaderboards.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderboardOutputDto>>> Get([FromQuery] PaginationDto pagination)
        {
            var all = await _leaderboardService.GetAll(pagination);

            return Ok(all);
        }

        [HttpPost]
        public async Task<IActionResult> Post(LeaderboardInputDto inputDto)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _leaderboardService.AddToLeaderboard(inputDto, userId);
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(LeaderboardUpdateDto updateDto)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _leaderboardService.UpdateLeaderboardItem(updateDto, userId);
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
