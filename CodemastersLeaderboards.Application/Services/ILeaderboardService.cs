using GameLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameLeaderboards.Application.Services
{
    public interface ILeaderboardService
    {
        Task<LeaderboardOutputDto> GetAll(PaginationDto pagination);

        Task AddToLeaderboard(LeaderboardInputDto inputDto, int userId);

        Task UpdateLeaderboardItem(LeaderboardUpdateDto updateDto, int userId);
    }
}
