using JWTAuthenticatonDemo.Application.Contracts.Services;
using JWTAuthenticatonDemo.Application.Models.Authentication;
using JWTAuthenticatonDemo.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Features.Authentication.Queries
{
    public class UserLoginQuery : IRequest<Response<AuthenticationResponse>>
    {
        public AuthenticationRequest AuthenticationParams { get; set; }
    }

    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, Response<AuthenticationResponse>>
    {
        private readonly IAuthenticationService _authenticationService;

        public UserLoginQueryHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Response<AuthenticationResponse>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            return await _authenticationService.AuthenticateUserAsync(request.AuthenticationParams);
        }
    }
}
