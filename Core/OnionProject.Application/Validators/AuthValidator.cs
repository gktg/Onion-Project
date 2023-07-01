using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnionProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProject.Application.Validators
{
    public class AuthValidator : AbstractValidator<Auth>
    {
        private static readonly AuthValidator Validator = new AuthValidator();

        private AuthValidator()
        {

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Telefon numarası boş olmamalıdır.")
                .EmailAddress();


            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Şifre boş olmamalıdır.")
                .NotEmpty()
                .WithMessage("Şifre boş olmamalıdır.")
                .MinimumLength(6)
                .WithName("Şifre")
                .MaximumLength(16)
                .WithName("Şifre");

        }


        public static AuthValidator GetValidation()
        {
            return Validator;
        }
    }
}
