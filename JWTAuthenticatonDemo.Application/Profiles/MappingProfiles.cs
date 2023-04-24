using AutoMapper;
using JWTAuthenticatonDemo.Application.Models.Authentication;
using JWTAuthenticatonDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, RegistrationResponse>();
            CreateMap<RegistrationRequest, ApplicationUser>();
            CreateMap<ApplicationUser, AuthenticationResponse>();
        }
    }
}
