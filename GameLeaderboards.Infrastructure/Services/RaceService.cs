using GameLeaderboards.Application.Services;
using GameLeaderboards.Application.Services.DomainService;
using GameLeaderboards.Domain.Models;
using GameLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;

namespace GameLeaderboards.Infrastructure.Services
{
    public class RaceService : IRaceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RaceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<RaceDto> GetAll()
        {
            var all = _unitOfWork.Repository<Race>().FindAll();

            // we can use automapper to map to dto model, but for simplicity we map it ourselves

            var dtos = new List<RaceDto>();
            foreach (var item in all)
            {
                dtos.Add(new RaceDto { Id = item.Id, LapCount = item.LapCount });
            }

            return dtos;
        }
    }
}
