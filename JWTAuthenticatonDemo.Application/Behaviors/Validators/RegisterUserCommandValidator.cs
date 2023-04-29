using FluentValidation;
using JWTAuthenticatonDemo.Application.Contracts.Repositories;
using JWTAuthenticatonDemo.Application.Features.ApplicationUser.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Behaviors.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(IApplicationUserRepository applicationUserRepo)
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
