using AutoMapper;
using JWTAuthenticatonDemo.Application.Features.ApplicationUser.Commands;
using JWTAuthenticatonDemo.Application.Features.ApplicationUser.Queries;
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
            CreateMap<RegisterUserCommand, RegistrationRequest>();
            CreateMap<LoginUserQuery, AuthenticationRequest>();
        }
    }
}
