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
    public class LoginUserQuery : IRequest<AuthenticationResponse>
    {
        //public AuthenticationRequest LoginUser { get; set; }
    }

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AuthenticationResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginUserQueryHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public Task<AuthenticationResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var result = _authenticationService.AuthenticateUserAsync();
            return result;
        }
    }
}
