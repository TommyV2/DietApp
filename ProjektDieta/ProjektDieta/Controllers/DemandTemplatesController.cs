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
    [Route("demand-templates")]
    public class DemandTemplatesController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IRepository<DemandTemplateModel> _templates;

        public DemandTemplatesController(ILogger<CustomersController> logger, IRepository<DemandTemplateModel> templates)
        {
            _logger = logger;
            _templates = templates;
        }

        [HttpGet]
        public IActionResult GetDemandTemplates([FromQuery] long specialistId)
        {
            var demands = _templates.GetAll().Where(demand => demand.SpecialistId == null || demand.SpecialistId == specialistId);

            var dtos = demands.Select(model => new DemandTemplateDto()
            {
                Id = model.Id,
                SpecialistId = model.SpecialistId,
                A = model.A,
                B6 = model.B6,
                Biotin = model.Biotin,
                C = model.C,
                Calcium = model.Calcium,
                Carbohydrates = model.Carbohydrates,
                Chlorine = model.Chlorine,
                Choline = model.Choline,
                Cobalamin = model.Cobalamin,
                Copper = model.Copper,
                D = model.D,
                E = model.E,
                Fat = model.Fat,
                Fibre = model.Fibre,
                Fluorine = model.Fluorine,
                Folate = model.Folate,
                Iodine = model.Iodine,
                Iron = model.Iron,
                K = model.K,
                Kcal = model.Kcal,
                Magnesium = model.Magnesium,
                Niacin = model.Niacin,
                PantothenicAcid = model.PantothenicAcid,
                Phosphorus = model.Phosphorus,
                Potassium = model.Potassium,
                Protein = model.Protein,
                Riboflavin = model.Phosphorus,
                Selenium = model.Selenium,
                Sodium = model.Sodium,
                Timine = model.Timine,
                Zinc = model.Zinc,
                Name = model.Name
            });

            return Ok(dtos);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DemandTemplateDto dto)
        {
            var demand = new DemandTemplateModel()
            {
                SpecialistId = dto.SpecialistId,
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
                Zinc = dto.Zinc,
                Name = dto.Name
            };

            _templates.Insert(demand);

            return Ok(demand.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(long id, [FromBody] DemandTemplateDto dto)
        {
            var template = _templates.GetById(id);

            template.A = dto.A ?? template.A;
            template.B6 = dto.B6 ?? template.B6;
            template.Biotin = dto.Biotin ?? template.Biotin;
            template.C = dto.C ?? template.C;
            template.Calcium = dto.Calcium ?? template.Calcium;
            template.Carbohydrates = dto.Carbohydrates ?? template.Carbohydrates;
            template.Chlorine = dto.Chlorine ?? template.Chlorine;
            template.Choline = dto.Choline ?? template.Choline;
            template.Cobalamin = dto.Cobalamin ?? template.Cobalamin;
            template.Copper = dto.Copper ?? template.Copper;
            template.D = dto.D ?? template.D;
            template.E = dto.E ?? template.E;
            template.Fat = dto.Fat ?? template.Fat;
            template.Fibre = dto.Fibre ?? template.Fibre;
            template.Fluorine = dto.Fluorine ?? template.Fluorine;
            template.Folate = dto.Folate ?? template.Folate;
            template.Iodine = dto.Iodine ?? template.Iodine;
            template.Iron = dto.Iron ?? template.Iron;
            template.K = dto.K ?? template.K;
            template.Kcal = dto.Kcal ?? template.Kcal;
            template.Magnesium = dto.Magnesium ?? template.Magnesium;
            template.Niacin = dto.Niacin ?? template.Niacin;
            template.PantothenicAcid = dto.PantothenicAcid ?? template.PantothenicAcid;
            template.Phosphorus = dto.Phosphorus ?? template.Phosphorus;
            template.Potassium = dto.Potassium ?? template.Potassium;
            template.Protein = dto.Protein ?? template.Protein;
            template.Riboflavin = dto.Phosphorus ?? template.Riboflavin;
            template.Selenium = dto.Selenium ?? template.Selenium;
            template.Sodium = dto.Sodium ?? template.Sodium;
            template.Timine = dto.Timine ?? template.Timine;
            template.Zinc = dto.Zinc ?? template.Zinc;
            template.Name = dto.Name ?? template.Name;

            _templates.Update(template);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id)
        {
            var template = _templates.GetById(id);
            _templates.Delete(template);
            return Ok();
        }
    }
}
