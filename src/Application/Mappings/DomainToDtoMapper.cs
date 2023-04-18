﻿using Application.Dtos;
using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mappings
{
    public class DomainToDtoMapper : Profile
    {
        public DomainToDtoMapper()
        {
            CreateMap<Team, TeamDto>();
            CreateMap<User, UserDto>();
        }
    }
}
