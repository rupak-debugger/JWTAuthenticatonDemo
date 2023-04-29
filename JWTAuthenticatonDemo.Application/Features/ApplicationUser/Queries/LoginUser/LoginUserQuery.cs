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

namespace JWTAuthenticatonDemo.Application.Features.ApplicationUser.Queries
{
    public class LoginUserQuery : IRequest<Response<AuthenticationResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Response<AuthenticationResponse>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public LoginUserQueryHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<Response<AuthenticationResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var @params = _mapper.Map<AuthenticationRequest>(request);
            return await _authenticationService.AuthenticateUserAsync(@params);
        }
    }
}
