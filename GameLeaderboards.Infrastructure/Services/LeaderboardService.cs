using GameLeaderboards.Application.CustomExceptions;
using GameLeaderboards.Application.Services;
using GameLeaderboards.Application.Services.DomainService;
using GameLeaderboards.Domain.Models;
using GameLeaderboards.Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLeaderboards.Infrastructure.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaderboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LeaderboardOutputDto> GetAll(PaginationDto pagination)
        {
            var all = _unitOfWork.Repository<Leaderboard>().FindAll();

            // filter by race
            if (pagination.RaceId != -1)
            {
                all = all.Where(x => x.RaceId == pagination.RaceId);
            }

            // filter by platform
            if (pagination.PlatformId != -1)
            {
                all = all.Where(x => x.PlatformId == pagination.PlatformId);
            }

            var totalCount = await all.CountAsync();

            var query = await all
                
                // pagination
                .OrderBy(on => on.Time)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)

                .Include(x => x.User)
                .Include(x => x.Country)
                .Include(x => x.Platform)
                .Include(x => x.Vehicle)

                .ToListAsync();

            // we can use automapper to map to dto model, but for simplicity we map it ourselves

            var dto = new LeaderboardOutputDto();

            dto.TotalCount = totalCount;

            foreach (var item in query)
            {
                var time = TimeSpan.FromMilliseconds(item.Time);

                dto.List.Add(new LeaderboardOutputDto.DataList
                {
                    Username = item.User.Username,
                    Country = item.Country.Name,
                    Platform = item.Platform.Name,
                    Vehicle = item.Vehicle.Name,
                    Time = $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}",
                    RaceId = item.RaceId
                });
            }

            return dto;
        }

        public async Task AddToLeaderboard(LeaderboardInputDto inputDto, int userId)
        {
            // we could bind country,vehicle,platform to user db model, but for simplicity we just get them from input

            var leaderboard = new Leaderboard
            {
                UserId = userId,
                RaceId = inputDto.RaceId,
                CountryId = inputDto.CountryId,
                VehicleId = inputDto.VehicleId,
                PlatformId = inputDto.PlatformId,
                Time = inputDto.Time
            };

            _unitOfWork.Repository<Leaderboard>().Create(leaderboard);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateLeaderboardItem(LeaderboardUpdateDto updateDto, int userId)
        {
            var leaderboard = await _unitOfWork.Repository<Leaderboard>()
                .FindByCondition(x => x.UserId == userId && x.RaceId == updateDto.RaceId).FirstOrDefaultAsync();

            if (leaderboard == null)
            {
                throw new EntityNotFoundException();
            }

            leaderboard.Time = updateDto.Time;

            _unitOfWork.Repository<Leaderboard>().Update(leaderboard);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
