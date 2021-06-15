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
    [Route("diet-recommendations")]
    public class DietRecommendationsController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IRepository<DietRecommendationModel> _recommendations;
        private readonly IRepository<CollaborationModel> _collaborations;
        private readonly IRepository<DemandModel> _demands;

        public DietRecommendationsController(ILogger<CustomersController> logger, IRepository<DietRecommendationModel> recommendations, IRepository<CollaborationModel> collaborations, IRepository<DemandModel> demands)
        {
            _logger = logger;
            _recommendations = recommendations;
            _collaborations = collaborations;
            _demands = demands;

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            var recommendation = _recommendations.GetById(id);
            if(recommendation == null)
            {
                return NotFound();
            }

            var dto = new DietRecommendationDto()
            {
                CollaborationId = recommendation.CollaborationId,
                Text = recommendation.Text,
                Id = recommendation.Id,
                SendDate = recommendation.SendDate
            };

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DietRecommendationDto dto)
        {
            var recommendation = new DietRecommendationModel()
            {
                CollaborationId = dto.CollaborationId,
                SendDate = dto.SendDate,
                Text = dto.Text,
                DemandsId = null
            };

            var collaboration = _collaborations.GetById((long)dto.CollaborationId);
            if (collaboration == null)
            {
                return BadRequest();
            }

            _recommendations.Insert(recommendation);

            return Ok(recommendation.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(long id, [FromBody] DietRecommendationDto dto)
        {
            var recommendation = _recommendations.GetById(id);
            if (recommendation == null)
                return NotFound();

            recommendation.SendDate = dto.SendDate ?? recommendation.SendDate;
            recommendation.Text = dto.Text ?? recommendation.Text;

            _recommendations.Update(recommendation);

            return Ok();
        }

        [HttpDelete]
        [Route("id")]
        public IActionResult Delete(long id)
        {
            var recommendation = _recommendations.GetById(id);
            _recommendations.Delete(recommendation);

            return Ok();
        }

        /*
        [HttpGet]
        [Route("{id}/demand")]
        public IActionResult GetDemand(long id)
        {
            var recommendation = _recommendations.GetById(id, p => p.DemandsModel);
            var demand = recommendation?.DemandsModel;
            if (recommendation == null || demand == null)
            {
                return BadRequest();
            }

            var dto = new DemandDto()
            {
                Id = demand.Id,
                A = demand.A,
                B6 = demand.B6,
                Biotin = demand.Biotin,
                C = demand.C,
                Calcium = demand.Calcium,
                Carbohydrates = demand.Carbohydrates,
                Chlorine = demand.Chlorine,
                Choline = demand.Choline,
                Cobalamin = demand.Cobalamin,
                Copper = demand.Copper,
                D = demand.D,
                E = demand.E,
                Fat = demand.Fat,
                Fibre = demand.Fibre,
                Fluorine = demand.Fluorine,
                Folate = demand.Folate,
                Iodine = demand.Iodine,
                Iron = demand.Iron,
                K = demand.K,
                Kcal = demand.Kcal,
                Magnesium = demand.Magnesium,
                Niacin = demand.Niacin,
                PantothenicAcid = demand.PantothenicAcid,
                Phosphorus = demand.Phosphorus,
                Potassium = demand.Potassium,
                Protein = demand.Protein,
                Riboflavin = demand.Phosphorus,
                Selenium = demand.Selenium,
                Sodium = demand.Sodium,
                Timine = demand.Timine,
                Zinc = demand.Zinc
            };

            return Ok(dto);
        }
        */

        /*
        [HttpPost]
        [Route("{id}/demand")]
        public IActionResult PostDemand(long id, [FromBody] DemandDto dto)
        {
            var recommendations = _recommendations.GetById((long)id);
            if (recommendations == null)
            {
                return BadRequest();
            }

            var demand = new DemandModel()
            {
                A = dto.A,
                B6 = dto.B6,
                Biotin = dto.Biotin,
                C = dto.C,
                Calcium = dto.Calcium,
                Carbohydrates = dto.Carbohydrates,
                Chlorine = dto.Chlorine,
                Choline = dto.Choline,
                Cobalamin = dto.Cobalamin,
                Copper = dto.Copper,
                D = dto.D,
                E = dto.E,
                Fat = dto.Fat,
                Fibre = dto.Fibre,
                Fluorine = dto.Fluorine,
                Folate = dto.Folate,
                Iodine = dto.Iodine,
                Iron = dto.Iron,
                K = dto.K,
                Kcal = dto.Kcal,
                Magnesium = dto.Magnesium,
                Niacin = dto.Niacin,
                PantothenicAcid = dto.PantothenicAcid,
                Phosphorus = dto.Phosphorus,
                Potassium = dto.Potassium,
                Protein = dto.Protein,
                Riboflavin = dto.Phosphorus,
                Selenium = dto.Selenium,
                Sodium = dto.Sodium,
                Timine = dto.Timine,
                Zinc = dto.Zinc
            };

            demand.DietRecommendations.Add(recommendations);
            _demands.Insert(demand);
            
            return Ok();
        }
        */

        //TODO del i put
    }
}
