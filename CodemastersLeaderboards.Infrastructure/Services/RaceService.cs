using CodemastersLeaderboards.Application.Services;
using CodemastersLeaderboards.Application.Services.DomainService;
using CodemastersLeaderboards.Domain.Models;
using CodemastersLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;

namespace CodemastersLeaderboards.Infrastructure.Services
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
