using CodemastersLeaderboards.Application.Services;
using CodemastersLeaderboards.Domain.Models.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodemastersLeaderboards.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CountryDto>> Get()
        {
            var all = _countryService.GetAll();
            return Ok(all);
        }
    }
}
