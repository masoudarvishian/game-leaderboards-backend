using CodemastersLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;

namespace CodemastersLeaderboards.Application.Services
{
    public interface ICountryService
    {
        IEnumerable<CountryDto> GetAll();
    }
}
