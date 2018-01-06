using System;
using AutoMapper;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<auth_user, AuthUserDto>();
            CreateMap<AuthUserDto, auth_user>();
        }
    }
}
