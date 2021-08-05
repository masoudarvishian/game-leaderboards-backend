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

        public IEnumerable<LeaderboardDto> GetAll()
        {
            var all = _unitOfWork.Repository<Leaderboard>().FindAll()
                .Include(x => x.User)
                .Include(x => x.Country)
                .Include(x => x.Platform)
                .Include(x => x.Vehicle);

            // we can use automapper to map to dto model, but for simplicity we map it ourselves

            var dtos = new List<LeaderboardDto>();

            foreach (var item in all)
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
