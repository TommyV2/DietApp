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

namespace ProjektDieta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialistsController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IRepository<SpecialistModel> _specialists;
        private readonly IRepository<CustomerModel> _customer;

        public SpecialistsController(ILogger<CustomersController> logger,
                                     IRepository<SpecialistModel> specialists,
                                     IRepository<CustomerModel> customer)
        {
            _logger = logger;
            _specialists = specialists;
            _customer = customer;
        }

        [HttpGet]
        public IActionResult GetSpecialists(string email)
        {
            //auth
            /*
            var authTokenId = 1;

            if (id != authTokenId)
                return Unauthorized();
            */

            var specialists = _specialists.GetAll();
            var filtered = email == null
                ? specialists
                : specialists.Where(model => model.Email.Contains(email));
            var dtos = filtered.Select(model => new SpecialistDto()
            {
                Id = model.Id,
                Username = model.Username,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Birthdate = model.BirthDate,
                City = model.City,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role
            });

            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSpecialist(long id)
        {

            var specialist = _specialists.GetById(id);
            if (specialist == null)
                return BadRequest();

            var dto = new SpecialistDto()
            {
                Id = specialist.Id,
                //Birthdate = specialist.BirthDate.ToString("dd.mm.yyyy"),
                Birthdate = specialist.BirthDate,
                City = specialist.City,
                Email = specialist.Email,
                Gender = specialist.Gender,
                Name = specialist.Name,
                PhoneNumber = specialist.PhoneNumber,
                Role = specialist.Role,
                Surname = specialist.Surname,
                Username = specialist.Username
            };
            return Ok(dto);
        }

        
        [HttpGet]
        [Route("{id}/collaborations")]
        public IActionResult GetCollaborations(long id)
        {
            var specialist = _specialists.GetById(id, p => p.Collaborations);
            if (specialist == null)
                return BadRequest();

            var collaborations = specialist.Collaborations.Select(model => new CollaborationDto()
            {
                Id = model.Id,
                SpecialistId = model.SpecialistId,
                SpecialistName = $"{specialist.Name} {specialist.Surname}",
                Status = model.Status,
                Type = model.Type,
                CustomerId = model.CustomerId
            });
            var collaborationList = collaborations.ToList();
            foreach(CollaborationDto coll in collaborationList)
            {
                CustomerModel cust = _customer.GetById((long) coll.CustomerId);
                coll.CustomerName = $"{cust.Name} {cust.Surname}";
            }

            return Ok(collaborationList);
        }

        [HttpGet]
        [Route("{id}/invitations")]
        public IActionResult GetInvitations(long id)
        {
            //autoryzacja

            var specialist = _specialists.GetById(id, p => p.Invitations);
            if (specialist == null)
                return BadRequest();

            var invitations = specialist.Invitations.Select(model => new InvitationDto()
            {
                Id = model.Id,
                CustomerId = model.CustomerId,
                SpecialistId = model.SpecialistId,
                SpecialistName = $"{specialist.Name} {specialist.Surname}",
                CustomerName = $"{_customer.GetById((long)model.CustomerId).Name} {_customer.GetById((long)model.CustomerId).Surname}",
                SendDate = model.SendDate,
                Type = model.Type,
                InvitedBy = model.InvitedBy
            });
            var invitationsList = invitations.ToList();
            /*foreach (InvitationDto invit in invitationsList)
            {
                CustomerModel cust = _customer.GetById((long)invit.CustomerId);
                invit.CustomerName = $"{cust.Name} {cust.Surname}";
            }
            */

            return Ok(invitationsList);
        }


        [HttpPost]
        public IActionResult Post([FromBody] SpecialistDto dto)
        {
            var specialist = new SpecialistModel()
            {
                Username = dto.Username,
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                //BirthDate = DateTime.Parse(dto.Birthdate),
                BirthDate = dto.Birthdate,
                City = dto.City,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber,
                Role = dto.Role
            };


            _specialists.Insert(specialist);
            return Ok(specialist.Id);
        }

        //TODO dodac not found
        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(long id, [FromBody] SpecialistDto dto)
        {
            var specialist = _specialists.GetById(id);

            specialist.BirthDate = dto.Birthdate ?? specialist.BirthDate;
            specialist.City = dto.City ?? specialist.City;
            specialist.Email = dto.Email ?? specialist.Email;
            specialist.Gender = dto.Gender ?? specialist.Gender;
            specialist.Name = dto.Name ?? specialist.Name;
            specialist.PhoneNumber = dto.PhoneNumber ?? specialist.PhoneNumber;
            specialist.Role = dto.Role ?? specialist.Role;
            specialist.Surname = dto.Surname ?? specialist.Surname;
            specialist.Username = dto.Username ?? specialist.Username;

            _specialists.Update(specialist);
            return Ok();
        }
        


        
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id)
        {
            var specialist = _specialists.GetById(id);
            _specialists.Delete(specialist);
            return Ok();
        }
        
    }
}
