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
        private readonly IApplicationUserRepository _applicationUserRepo;
        public RegisterUserCommandValidator(IApplicationUserRepository applicationUserRepo)
        {
            _applicationUserRepo = applicationUserRepo;
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(u => u.LastName)
                .NotEmpty()
                .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(u => u.Email)
                .NotEmpty()
                .MustAsync(async (email,token) =>
                !await _applicationUserRepo.AnyAsync(x => x.Email == email))
                .WithMessage((email)=>$" Email: '{email}' Already taken");
        }
    }
}
