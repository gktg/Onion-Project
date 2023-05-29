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

            RuleFor(x => x.Phone)
                .NotNull()
                .NotEmpty()
                .WithMessage("Telefon numarası boş olmamalıdır.");


            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Şifre boş olmamalıdır.");

        }


        public static AuthValidator GetValidation()
        {
            return Validator;
        }
    }
}
