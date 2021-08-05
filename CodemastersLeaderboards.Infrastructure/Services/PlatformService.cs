using CodemastersLeaderboards.Application.Services;
using CodemastersLeaderboards.Application.Services.DomainService;
using CodemastersLeaderboards.Domain.Models;
using CodemastersLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;

namespace CodemastersLeaderboards.Infrastructure.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlatformService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlatformDto> GetAll()
        {
            var all = _unitOfWork.Repository<Platform>().FindAll();

            // we can use automapper to map to dto model, but for simplicity we map it ourselves

            List<PlatformDto> dtos = new List<PlatformDto>();
            foreach (var item in all)
            {
                dtos.Add(new PlatformDto { Id = item.Id, Name = item.Name });
            }

            return dtos;
        }
    }
}
