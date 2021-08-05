using CodemastersLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;

namespace CodemastersLeaderboards.Application.Services
{
    public interface IRaceService
    {
        IEnumerable<RaceDto> GetAll();
    }
}
