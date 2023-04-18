using JWTAuthenticatonDemo.Application.Models.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Features.Login.Commands
{
    public class LoginUserCommand : IRequest<AuthenticationResponse>
    {
    }
}
