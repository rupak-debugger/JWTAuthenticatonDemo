using AutoMapper;
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
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<Response<RegistrationResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUser>(request.RegistrationParams);
            var result = await _authenticationService.RegisterUserAsync(user);
            var response = _mapper.Map<RegistrationResponse>(result);
            return new Response<RegistrationResponse>(response, "User created successfully");
        }
    }
}
