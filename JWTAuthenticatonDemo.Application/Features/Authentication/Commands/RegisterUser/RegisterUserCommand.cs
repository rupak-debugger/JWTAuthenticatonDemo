using AutoMapper;
using JWTAuthenticatonDemo.Application.Common.Interfaces;
using JWTAuthenticatonDemo.Application.Contracts.Services;
using JWTAuthenticatonDemo.Application.Models.Authentication;
using JWTAuthenticatonDemo.Application.Wrappers;
using JWTAuthenticatonDemo.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Features.Authentication.Commands
{
    public class RegisterUserCommand : IRequest<Response<RegistrationResponse>>
    {
        public RegistrationRequest RegistrationParams { get; set; }
    }


    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<RegistrationResponse>>
    {
        private readonly IAuthenticationService _authenticationService;

        public RegisterUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Response<RegistrationResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.RegisterUserAsync(request.RegistrationParams);
        }
    }
}
