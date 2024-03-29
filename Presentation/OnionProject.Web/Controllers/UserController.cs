﻿using Microsoft.AspNetCore.Mvc;
using OnionProject.Application.Repositories;
using OnionProject.Application.Validators;
using OnionProject.Domain.Entities;
using FluentValidation.Results;
using OnionProject.Infrastructure.Tools;
using Microsoft.AspNetCore.Authentication;
using OnionProject.Domain.Entities.Models;

namespace OnionProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public UserController(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }


        [HttpPost]
        [Route("NewUser")]
        public Session NewUser(Auth auth)
        {
            Session session = new Session();

            AuthValidator authValidator = AuthValidator.GetValidation();

            ValidationResult validationResult = authValidator.Validate(auth);

            if (validationResult.IsValid)
            {
                try
                {
                    Customer? emailControl = _customerReadRepository.GetWhere(x => x.Email == auth.Email).FirstOrDefault();

                    if (emailControl == null)
                    {
                        string hasspass = HashPass.hashPass(auth.Password);

                        Customer customer = new Customer
                        {
                            Password = hasspass,
                            Email = auth.Email,
                            IsActive = true,
                        };

                        _customerWriteRepository.Add(customer);
                        var newCustomer = _customerWriteRepository.Save();

                        if (newCustomer == 1)
                        {
                            session.SuccessList = new List<string> { "Üyeliğiniz başarıyla oluşturulmuştur." };
                            session.HasError = false;

                            try
                            {

                                Customer? login = _customerReadRepository.GetWhere(x => x.Email == auth.Email && x.Password == HashPass.hashPass(auth.Password)).FirstOrDefault();

                                if (login != null)
                                {
                                    if (login.IsActive)
                                    {
                                        session.SessionId = Guid.NewGuid().ToString() + "-" + login.Id.ToString();
                                        return session;
                                    }
                                    else
                                    {
                                        session.ErrorList = new List<string> { "Kullanıcı aktif değil" };
                                        return session;

                                    }

                                }
                                else
                                {
                                    session.ErrorList = new List<string> { "Giriş başarısız (Kullanıcı adı veya şifre hatalı)" };
                                    session.HasError = true;
                                    return session;

                                }


                            }
                            catch (Exception e)
                            {
                                session.ErrorList = new List<string> { e.Message };
                                return session;
                            }
                        }
                        else
                        {
                            session.ErrorList = new List<string> { "Sistemsel bir hata oluştu, lütfen tekrar deneyiniz" };
                            session.HasError = true;

                            return session;
                        }

                    }
                    else
                    {
                        session.ErrorList = new List<string> { "Email ile üyelik bulunmaktadır" };
                        session.HasError = true;
                        return session;

                    }
                }
                catch (Exception e)
                {

                    session.ErrorList = new List<string> { e.Message };
                    return session;
                }

            }
            else
            {
                List<string> errorMesages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                session.HasError = true;
                session.ErrorList = errorMesages;
                return session;

            }




        }
    }
}
