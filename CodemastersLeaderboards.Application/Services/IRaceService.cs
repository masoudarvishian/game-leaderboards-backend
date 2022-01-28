using GameLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;

namespace GameLeaderboards.Application.Services
{
    public interface IRaceService
    {
        IEnumerable<RaceDto> GetAll();
    }
}
