using JWTAuthenticatonDemo.Application.Models.Authentication;
using JWTAuthenticatonDemo.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Features.Authentication.Commands
{
    public class AddUserCommand : IRequest<Response<RegistrationResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Response<RegistrationResponse>>
    {
        public Task<Response<RegistrationResponse>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
