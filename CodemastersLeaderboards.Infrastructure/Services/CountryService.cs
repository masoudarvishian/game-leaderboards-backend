using CodemastersLeaderboards.Application.Services;
using CodemastersLeaderboards.Application.Services.DomainService;
using CodemastersLeaderboards.Domain.Models;
using CodemastersLeaderboards.Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodemastersLeaderboards.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CountryDto> GetAll()
        {
            var all = _unitOfWork.Repository<Country>().FindAll();

            // we can use automapper to map to dto model, but for simplicity we map it ourselves

            var dtos = new List<CountryDto>();
            foreach (var item in all)
            {
                dtos.Add(new CountryDto { Id = item.Id, Name = item.Name });
            }

            return dtos;
        }
    }
}
