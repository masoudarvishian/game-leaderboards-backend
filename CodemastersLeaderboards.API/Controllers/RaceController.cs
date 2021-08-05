using CodemastersLeaderboards.Application.Services;
using CodemastersLeaderboards.Domain.Models.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodemastersLeaderboards.API.Controllers
{
    // we can config this controller so only admin users can see the list of all races, but for simplicity
    // we imagine every logged in user can retrieve the list of races

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly IRaceService _raceService;

        public RaceController(IRaceService raceService)
        {
            _raceService = raceService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RaceDto>> Get()
        {
            var all = _raceService.GetAll();
            return Ok(all);
        }
    }
}
