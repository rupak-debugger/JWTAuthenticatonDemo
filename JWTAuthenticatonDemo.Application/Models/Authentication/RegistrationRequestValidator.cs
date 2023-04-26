using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Models.Authentication
{
    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .NotEqual("string")
                .NotNull();
            RuleFor(r => r.LastName)
                .NotEqual("string");
        }
    }
}
