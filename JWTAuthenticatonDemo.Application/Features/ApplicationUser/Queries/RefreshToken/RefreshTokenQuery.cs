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
    public class RefreshTokenQuery : IRequest<Response<AuthenticationResponse>>
    {
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenueryHandler : IRequestHandler<RefreshTokenQuery, Response<AuthenticationResponse>>
    {
        private readonly IAuthenticationService _authenticationService;

        public RefreshTokenueryHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<Response<AuthenticationResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            return await _authenticationService.RefreshTokenAsync(request.RefreshToken);            
        }
    }
}
