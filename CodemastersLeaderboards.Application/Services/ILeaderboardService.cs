using CodemastersLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodemastersLeaderboards.Application.Services
{
    public interface ILeaderboardService
    {
        Task<IEnumerable<LeaderboardOutputDto>> GetAll(PaginationDto pagination);

        Task AddToLeaderboard(LeaderboardInputDto inputDto, int userId);

        Task UpdateLeaderboardItem(LeaderboardUpdateDto updateDto, int userId);
    }
}
