using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.JSInterop;
using ProjektDieta.Dtos;
using ProjektDieta.Models;
using ProjektDieta.Repository;

namespace ProjektDieta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IRepository<CustomerModel> _customers;
        private readonly IRepository<SpecialistModel> _specialists;
        public CustomersController(ILogger<CustomersController> logger,
                                   IRepository<CustomerModel> customers,
                                   IRepository<SpecialistModel> specialists)
        {
            _logger = logger;
            _customers = customers;
            _specialists = specialists;
        }

        [HttpGet]
        public IActionResult GetCustomers(string email)
        {
            //auth
            /*
            var authTokenId = 1;

            if (id != authTokenId)
                return Unauthorized();
            */
            
            var customers = _customers.GetAll();
            var filtered = email == null
                ? customers
                : customers.Where(model => model.Email.Contains(email));
            var dtos = filtered.Select(model => new CustomerDto()
            {
                Id = model.Id,
                Username = model.Username,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Birthdate = model.BirthDate,
                City = model.City,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber
            });
            

            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCustomer(long id)
        {
            //auth
            /*
            var authTokenId = 1;

            if (id != authTokenId)
                return Unauthorized();
            */

            var customer = _customers.GetById(id);
            if (customer == null)
                return BadRequest();

            var dto = new CustomerDto()
            {
                Id = customer.Id,
                Username = customer.Username,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                //Birthdate = customer.BirthDate.ToString("dd.mm.yyyy"),
                Birthdate = customer.BirthDate,
                City = customer.City,
                Gender = customer.Gender,
                PhoneNumber = customer.PhoneNumber
            };

            return Ok(dto);
        }

        [HttpGet]
        [Route("{id}/collaborations")]
        public IActionResult GetCollaborations(long id)
        {
            //auth
            /*
            var authTokenId = 1;

            if (id != authTokenId)
                return Unauthorized();
            */

            var customer = _customers.GetById(id, p => p.Collaborations);
            if (customer == null)
                return BadRequest();


            var collaborations = customer.Collaborations.Select(model => new CollaborationDto()
            {
                Id = model.Id,
                SpecialistId = model.SpecialistId,
                CustomerName = $"{customer.Name} {customer.Surname}",
                Status = model.Status,
                Type = model.Type,
                CustomerId = model.CustomerId
            });
            var collaborationList = collaborations.ToList();
            foreach (CollaborationDto coll in collaborationList)
            {
                SpecialistModel spec = _specialists.GetById((long)coll.SpecialistId);
                coll.SpecialistName = $"{spec.Name} {spec.Surname}";
            }
            return Ok(collaborationList);
        }

        [HttpGet]
        [Route("{id}/invitations")]
        public IActionResult GetInvitations(long id)
        {
            //autoryzacja

            var customer = _customers.GetById(id, p => p.Invitations);
            if (customer == null)
                return BadRequest();

            var invitations = customer.Invitations.Select(model => new InvitationDto()
            {
                Id = model.Id,
                CustomerId = model.CustomerId,
                SpecialistId = model.SpecialistId,
                CustomerName = $"{customer.Name} {customer.Surname}",
                SendDate = model.SendDate,
                Type = model.Type,
                InvitedBy = model.InvitedBy
            });
            var invitationsList = invitations.ToList();
            foreach (InvitationDto invit in invitationsList)
            {
                SpecialistModel spec = _specialists.GetById((long)invit.SpecialistId);
                invit.SpecialistName = $"{spec.Name} {spec.Surname}";
            }
            return Ok(invitationsList);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CustomerDto dto)
        {
            var customer = new CustomerModel()
            {
                Username = dto.Username,
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                //BirthDate = DateTime.Parse(dto.Birthdate),
                BirthDate = dto.Birthdate,
                City = dto.City,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber
            };


            _customers.Insert(customer);
            return Ok(customer.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(long id, [FromBody] CustomerDto dto)
        {
            var customer = _customers.GetById(id);

            customer.BirthDate = dto.Birthdate ?? customer.BirthDate;
            customer.City = dto.City ?? customer.City;
            customer.Email = dto.Email ?? customer.Email;
            customer.Gender = dto.Gender ?? customer.Gender;
            customer.Name = dto.Name ?? customer.Name;
            customer.PhoneNumber = dto.PhoneNumber ?? customer.PhoneNumber;
            customer.Surname = dto.Surname ?? customer.Surname;
            customer.Username = dto.Username ?? customer.Username;
            
            _customers.Update(customer);
            return Ok();
        }


        
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id)
        {
            //auth
            /*
            var authTokenId = 1;

            if (id != authTokenId)
                return Unauthorized();
            */
            var customer = _customers.GetById(id);
            _customers.Delete(customer);

            return Ok();
        }

        
    }
}
