using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjektDieta.Dtos;
using ProjektDieta.Models;
using ProjektDieta.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvitationsController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IRepository<CustomerModel> _customers;
        private readonly IRepository<SpecialistModel> _specialists;
        private readonly IRepository<InvitationModel> _invitations;
        private readonly IRepository<CollaborationModel> _collaborations;

        public InvitationsController(ILogger<CustomersController> logger,
                                        IRepository<CustomerModel> customers,
                                        IRepository<InvitationModel> invitations,
                                        IRepository<SpecialistModel> specialists,
                                        IRepository<CollaborationModel> collabortions)
        {
            _logger = logger;
            _customers = customers;
            _invitations = invitations;
            _specialists = specialists;
            _collaborations = collabortions;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            var invitations = _invitations.GetById(id);

            if (invitations == null)
                return BadRequest();


            //do zmiany?
            var customer = _customers.GetById((long)invitations.CustomerId);
            var specialist = _specialists.GetById((long)invitations.SpecialistId);
            if (customer == null || specialist == null)
                return BadRequest();


            var dto = new InvitationDto()
            {
                Id = invitations.Id,
                CustomerId = invitations.CustomerId,
                SpecialistId = invitations.SpecialistId,
                SendDate = invitations.SendDate,
                SpecialistName = $"{customer.Name} {customer.Surname}",
                CustomerName = $"{specialist.Name} {specialist.Surname}",
                Type = invitations.Type,
                InvitedBy = invitations.InvitedBy
            };
            return Ok(dto);
        }
        [HttpPost]
        public IActionResult Post([FromBody] InvitationDto dto)
        {
            //TODO
            //Waryfikacja uprawnień
            var invitation = new InvitationModel()
            {
                CustomerId = dto.CustomerId,
                SpecialistId = dto.SpecialistId,
                Type = dto.Type,
                InvitedBy = dto.InvitedBy
            };
            invitation.SendDate = DateTime.Today;

            var customer = _customers.GetById((long)dto.CustomerId);
            var specialist = _specialists.GetById((long)dto.SpecialistId);

            if (customer == null || specialist == null)
                return BadRequest();

            invitation.Customer = customer;
            invitation.Specialist = specialist;
            _invitations.Insert(invitation);

            return Ok(invitation.Id);
        }

        [HttpPost]
        [Route("{id}")]
        public IActionResult Post(long id, string action)
        {
            //TODO
            //Waryfikacja uprawnień
            var invitation = _invitations.GetById(id);

            if (invitation == null)
                return BadRequest();

            if (action == "accept")
            {
                // nie wiem czy potrzebne zaproszenie powinno i tak skasować się gdyby nie było kogoś w bazie
                var customer = _customers.GetById((long)invitation.CustomerId);
                var specialist = _specialists.GetById((long)invitation.SpecialistId);
                if (customer == null || specialist == null)
                    return BadRequest();
                
                var collaboration = new CollaborationModel() {
                    SpecialistId = invitation.SpecialistId,
                    CustomerId = invitation.CustomerId,
                    Type = invitation.Type,
                    Status = "active"
                };
                collaboration.Customer = customer;
                collaboration.Specialist = specialist;
                _collaborations.Insert(collaboration);
                _invitations.Delete(invitation);
                return Ok("Collaboration was created!");
            }
            else if (action == "delete")
            {
                _invitations.Delete(invitation);
                return Ok("Invitation was deleted!");
            }
            else
                return BadRequest("This action is forbiden!");

            
        }
    }
}
