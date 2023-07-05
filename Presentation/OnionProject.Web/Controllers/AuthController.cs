using Microsoft.AspNetCore.Mvc;
using OnionProject.Application.Repositories;
using OnionProject.Application.Validators;
using OnionProject.Domain.Entities;
using FluentValidation.Results;
using OnionProject.Infrastructure.Tools;
using OnionProject.Domain.Entities.Models;

namespace OnionProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public AuthController(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }



        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Session))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(Auth auth)
        {
            Session session = new Session();

            AuthValidator authValidator = AuthValidator.GetValidation();

            ValidationResult validationResult = authValidator.Validate(auth);

            if (validationResult.IsValid)
            {
                try
                {

                    Customer? login = _customerReadRepository.GetWhere(x=>x.Email == auth.Email && x.Password == HashPass.hashPass(auth.Password)).FirstOrDefault();

                    if (login != null)
                    {
                        if (login.IsActive)
                        {
                            session.SessionId = Guid.NewGuid().ToString() +"-"+ login.Id.ToString();
                            session.HasError = false;

                            return Ok(session);
                        }
                        else
                        {
                            session.ErrorList = new List<string> { "Kullanıcı aktif değil" };
                            session.HasError = true;
                            return StatusCode(404,session);

                        }

                    }
                    else
                    {
                        session.ErrorList = new List<string> { "Giriş başarısız (Kullanıcı adı veya şifre hatalı)" };
                        session.HasError = true;
                        return StatusCode(404, session);

                    }


                }
                catch (Exception e)
                {
                    session.ErrorList = new List<string> { e.Message };
                    return StatusCode(500,session);
                }

            }
            else
            {
                List<string> errorMesages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                session.HasError = true;
                session.ErrorList = errorMesages;
                return StatusCode(400,session);

            }

        }
    }
}
