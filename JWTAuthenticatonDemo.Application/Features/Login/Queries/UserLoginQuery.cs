using JWTAuthenticatonDemo.Application.Contracts.Services;
using JWTAuthenticatonDemo.Application.Models.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Features.Login.Queries
{
    public class UserLoginQuery : IRequest<AuthenticationResponse>
    {
        //public AuthenticationRequest LoginParams { get; set; }
    }

    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, AuthenticationResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public UserLoginQueryHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public Task<AuthenticationResponse> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var result = _authenticationService.AuthenticateUserAsync();
            return result;
        }
    }
}
