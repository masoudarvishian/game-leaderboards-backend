using CodemastersLeaderboards.Application.Services;
using CodemastersLeaderboards.Application.Services.DomainService;
using CodemastersLeaderboards.Domain.Models;
using CodemastersLeaderboards.Domain.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodemastersLeaderboards.Infrastructure.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaderboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<LeaderboardDto> GetAll(PaginationDto pagination)
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

            var query = all
                .Include(x => x.User)
                .Include(x => x.Country)
                .Include(x => x.Platform)
                .Include(x => x.Vehicle)
                // pagination
                .OrderBy(on => on.Time)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            // we can use automapper to map to dto model, but for simplicity we map it ourselves

            var dtos = new List<LeaderboardDto>();

            foreach (var item in query)
            {
                var time = TimeSpan.FromMilliseconds(item.Time);

                dtos.Add(new LeaderboardDto
                {
                    Username = item.User.Username,
                    Country = item.Country.Name,
                    Platform = item.Platform.Name,
                    Vehicle = item.Vehicle.Name,
                    Time = $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}"
                });
            }

            return dtos;
        }
    }
}
