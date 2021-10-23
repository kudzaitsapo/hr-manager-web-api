using AutoMapper;
using HrMan.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrMan.Controllers.Authentication.Dtos
{
    public class AuthMapperProfile : Profile
    {
        public AuthMapperProfile()
        {
            CreateMap<AppUser, UserDto>();

            CreateMap<RegistrationRequestDto, AppUser>()
                .ForMember(u => u.PasswordHash, options => options.Ignore())
                .ForMember(u => u.UserName, options => options.MapFrom(x => x.Email));

        }
    }
}
