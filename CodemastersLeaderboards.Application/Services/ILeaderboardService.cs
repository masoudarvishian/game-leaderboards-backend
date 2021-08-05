using CodemastersLeaderboards.Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodemastersLeaderboards.Application.Services
{
    public interface ILeaderboardService
    {
        IEnumerable<LeaderboardDto> GetAll();
    }
}
