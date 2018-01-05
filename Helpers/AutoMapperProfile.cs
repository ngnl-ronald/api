﻿using System;
using AutoMapper;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthUser, AuthUserDto>();
            CreateMap<AuthUserDto, AuthUser>();
        }
    }
}
