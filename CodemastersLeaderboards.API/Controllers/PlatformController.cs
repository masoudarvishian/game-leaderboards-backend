using GameLeaderboards.Application.Services;
using GameLeaderboards.Domain.Models.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameLeaderboards.API.Controllers
{
    // we can config this controller so only admin platforms can see the list of all races, but for simplicity
    // we imagine every logged in user can retrieve the list of platforms

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService _platformService;

        public PlatformController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformDto>> Get()
        {
            var all = _platformService.GetAll();
            return Ok(all);
        }
    }
}
