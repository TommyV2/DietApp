using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjektDieta.Dtos;
using ProjektDieta.Models;
using ProjektDieta.Repository;
using ProjektDieta.Extensions;
using ProjektDieta.Utils;

namespace ProjektDieta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollaborationsController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IRepository<CustomerModel> _customers;
        private readonly IRepository<SpecialistModel> _specialists;
        private readonly IRepository<CollaborationModel> _collaborations;

        public CollaborationsController(ILogger<CustomersController> logger, IRepository<CustomerModel> customers, IRepository<CollaborationModel> collaborations, IRepository<SpecialistModel> specialists)
        {
            _logger = logger;
            _customers = customers;
            _collaborations = collaborations;
            _specialists = specialists;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            /*
            if (HttpContext.Session.GetLong(SessionKeys.CustomerId) == 0)
                return Unauthorized();
            */

            var collaboration = _collaborations.GetById(id);
            if (collaboration == null)
                return BadRequest();

            var dto = new CollaborationDto()
            {
                Id = collaboration.Id,
                CustomerId = collaboration.CustomerId,
                SpecialistId = collaboration.SpecialistId,
                Type = collaboration.Type,
                Status = collaboration.Status
            };
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CollaborationDto dto)
        {
            var collaboration = new CollaborationModel()
            {
                Status = "active", //zmienic
                Type = dto.Type,
                CustomerId = dto.CustomerId,
                SpecialistId = dto.SpecialistId
            };


            var customer = _customers.GetById((long)dto.CustomerId);
            var specialist = _specialists.GetById((long)dto.SpecialistId);
            if (customer == null || specialist == null)
                return BadRequest();

            collaboration.Customer = customer;
            collaboration.Specialist = specialist;

            _collaborations.Insert(collaboration);

            return Ok(collaboration.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(long id, [FromQuery] string status)
        {
            var collaboration = _collaborations.GetById(id);

            //collaboration.CustomerId = dto.CustomerId ?? collaboration.CustomerId;
            //collaboration.SpecialistId = dto.SpecialistId ?? collaboration.SpecialistId;
            collaboration.Status = status ?? collaboration.Status;
            //collaboration.Type = dto.Type ?? collaboration.Type;

            _collaborations.Update(collaboration);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id)
        {
            var collaboration = _collaborations.GetById(id);
            _collaborations.Delete(collaboration);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/recommendations")]
        public IActionResult GetRecommendations(long id)
        {
            var collaboration = _collaborations.GetById(id, p => p.DietRecommendations);
            if(collaboration == null)
            {
                return BadRequest();
            }

            var dtos = collaboration.DietRecommendations.Select(model => new DietRecommendationDto()
            {
                Id = model.Id,
                CollaborationId = model.CollaborationId,
                SendDate = model.SendDate,
                Text = model.Text
            });

            return Ok(dtos);
        }

        //TODO dodac on delete do bazy danych

    }
}
