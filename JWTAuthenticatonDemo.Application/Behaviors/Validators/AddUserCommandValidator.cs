using FluentValidation;
using JWTAuthenticatonDemo.Application.Contracts.Repositories;
using JWTAuthenticatonDemo.Application.Features.Authentication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Behaviors.Validators
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator(IApplicationUserRepository applicationUserRepo)
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .NotNull()
                .NotEqual("string");

            RuleFor(u => u.LastName)
                .NotEmpty()
                .NotNull()
                .NotEqual("string");
        }
    }
}
