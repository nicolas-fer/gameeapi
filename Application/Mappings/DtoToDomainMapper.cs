using Application.Dtos;
using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mappings
{
    public class DtoToDomainMapper : Profile
    {
        public DtoToDomainMapper()
        {
            CreateMap<TeamDto, Team>();
            CreateMap<UserDto, User>();
        }
    }
}
