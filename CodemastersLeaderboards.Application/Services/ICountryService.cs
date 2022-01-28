using GameLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;

namespace GameLeaderboards.Application.Services
{
    public interface ICountryService
    {
        IEnumerable<CountryDto> GetAll();
    }
}
