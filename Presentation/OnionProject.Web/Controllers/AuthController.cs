using Microsoft.AspNetCore.Mvc;
using OnionProject.Application.Repositories;
using OnionProject.Application.Validators;
using OnionProject.Domain.Entities;
using FluentValidation.Results;

namespace OnionProject.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public AuthController(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        public Session Login(Auth auth)
        {
            Session session = new Session();

            AuthValidator authValidator = AuthValidator.GetValidation();

            ValidationResult validationResult = authValidator.Validate(auth);

            if (validationResult.IsValid)
            {

            }
            else
            {
                List<string> errorMesages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                session.HasError = true;
                session.Error = errorMesages;

            }


            return session;
        }
    }
}
