using CodemastersLeaderboards.Application.Services;
using CodemastersLeaderboards.Application.Services.DomainService;
using CodemastersLeaderboards.Domain.Models;
using CodemastersLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;

namespace CodemastersLeaderboards.Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<VehicleDto> GetAll()
        {
            var all = _unitOfWork.Repository<Vehicle>().FindAll();

            // we can use automapper to map to dto model, but for simplicity we map it ourselves

            var dtos = new List<VehicleDto>();

            foreach (var item in all)
            {
                dtos.Add(new VehicleDto { Id = item.Id, Name = item.Name });
            }

            return dtos;
        }
    }
}
