using AutoMapper;
using JWTAuthenticatonDemo.Application.Contracts.Services;
using JWTAuthenticatonDemo.Application.Models.Authentication;
using JWTAuthenticatonDemo.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Features.ApplicationUser.Commands
{
    public class RegisterUserCommand : IRequest<Response<RegistrationResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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
            var @params = _mapper.Map<RegistrationRequest>(request);
            return await _authenticationService.RegisterUserAsync(@params);
        }
    }
}
