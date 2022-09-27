using Application.Dtos;
using AutoMapper;
using Domain.Models;

namespace Application.Mappings
{
    public class DtoToDomainMapper : Profile
    {
        public DtoToDomainMapper()
        {
            CreateMap<TeamDto, Team>();
        }
    }
}
