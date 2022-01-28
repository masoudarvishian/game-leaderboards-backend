using GameLeaderboards.Domain.Models.Dto;
using System.Collections.Generic;

namespace GameLeaderboards.Application.Services
{
    public interface IVehicleService
    {
        IEnumerable<VehicleDto> GetAll();
    }
}
