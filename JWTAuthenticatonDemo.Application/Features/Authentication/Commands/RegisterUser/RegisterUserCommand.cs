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
    public class RegisterUserCommand : IRequest<Response<ApplicationUser>>
    {
        public ApplicationUser RegistrationParams { get; set; }
    }


    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<ApplicationUser>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<Response<ApplicationUser>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //var user = _mapper.Map<ApplicationUser>(request.RegistrationParams);
            var result = await _authenticationService.RegisterUserAsync(request.RegistrationParams);
            //var response = _mapper.Map<RegistrationResponse>(result);
            return new Response<ApplicationUser>(result, "User created successfully");
        }
    }
}
