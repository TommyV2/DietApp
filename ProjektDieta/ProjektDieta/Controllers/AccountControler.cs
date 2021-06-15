using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjektDieta.Dtos;
using ProjektDieta.Models;
using ProjektDieta.Repository;
using ProjektDieta.Enums;
using ProjektDieta.Utils;
using ProjektDieta.Extensions;

namespace ProjektDieta.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IRepository<AccountModel> _accounts;
        private readonly IRepository<CustomerModel> _customers;
        private readonly IRepository<SpecialistModel> _specialists;

        public AccountController(
            ILogger<CustomersController> logger,
            IRepository<AccountModel> accounts,
            IRepository<CustomerModel> customers,
            IRepository<SpecialistModel> specialists)
        {
            _logger = logger;
            _accounts = accounts;
            _customers = customers;
            _specialists = specialists;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegistrationRequestDto dto)
        {
            if ((dto.AccountRole != "customer") && (dto.AccountRole != "specialist"))
                return BadRequest();

            var status = CheckRegistrationStatus(dto.Email);

            if(status == RegistrationStatus.NotRegistered)
            {
                var newAccount = new AccountModel()
                {
                    Email = dto.Email,
                    Password = dto.Password
                };

                CreateAccountUserByRepo(newAccount, dto.Name, dto.Surname, dto.Email, dto.AccountRole);
                _accounts.Insert(newAccount);

                return Ok();

            }else if(status == RegistrationStatus.PartiallyRegistered)
            {
                var existingAccount = _accounts.GetAll().FirstOrDefault(account => account.Email == dto.Email);

                if (existingAccount.Password != dto.Password)
                    return Conflict();

                if((existingAccount.CustomerId != null) && dto.AccountRole == "customer")
                    return Conflict();

                if ((existingAccount.SpecialistId != null) && dto.AccountRole == "specialist")
                    return Conflict();


                CreateAccountUserByRepo(existingAccount, dto.Name, dto.Surname, dto.Email, dto.AccountRole);
                _accounts.Update(existingAccount);

                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            HttpContext.Session.Clear();

            //TODO zmienic status z BadRequest na Forbid
            var account = _accounts.GetAll().FirstOrDefault(account => account.Email == dto.Email);
            if (account == null)
                return BadRequest();

            if (account.Password != dto.Password)
                return BadRequest();

            var res = new LoginResponseDto();

            if (dto.AccountRole == "customer" && account.CustomerId != null)
            {
                HttpContext.Session.SetLong(SessionKeys.CustomerId, (long)account.CustomerId);
                res.CustomerId = account.CustomerId;
                return Ok(res);
            }

            if (dto.AccountRole == "specialist" && account.SpecialistId != null)
            {
                HttpContext.Session.SetLong(SessionKeys.SpecialistId, (long)account.SpecialistId);
                res.SpecialistId = account.SpecialistId;
                return Ok(res);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok();
        }

        private RegistrationStatus CheckRegistrationStatus(string email)
        {
            var account = _accounts.GetAll().FirstOrDefault(account => account.Email == email);

            if(account == null)
            {
                return RegistrationStatus.NotRegistered;
            }
            else
            {
                if (account.SpecialistId != null && account.CustomerId != null)
                    return RegistrationStatus.FullyRegistered;
                else
                    return RegistrationStatus.PartiallyRegistered;
            }

            
        }

        private void CreateAccountUserByRepo(AccountModel account, string name, string surname, string email, string role)
        {
            if (role == "customer")
            {
                var customer = new CustomerModel()
                {
                    Name = name,
                    Surname = surname,
                    Email = email
                };

                _customers.Insert(customer);
                account.CustomerId = customer.Id;
            }
            else
            {
                var specialist = new SpecialistModel()
                {
                    Name = name,
                    Surname = surname,
                    Email = email
                };

                _specialists.Insert(specialist);
                account.SpecialistId = specialist.Id;
            }
        }
    }
}
