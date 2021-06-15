using Microsoft.AspNetCore.Http;
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
    [Route("[controller]")]
    [ApiController]
    public class NutrientsController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IRepository<NutrientModel> _nutrients;

        public NutrientsController(ILogger<CustomersController> logger,
                                   IRepository<NutrientModel> nutrients)
        {
            _logger = logger;
            _nutrients = nutrients;
        }

        // GET
        [HttpGet]
        public IActionResult Get(string name)
        {
            var nutrients = _nutrients.GetAll();
            var filtered = name == null
                ? nutrients
                : nutrients.Where(model => model.Name.Contains(name));
            var selectedNutrients = filtered.Select(model => new NutrientDto()
            {
                Id = model.Id,
                Name = model.Name,
                PolName = model.PolName,
                ShortName = model.ShortName,
                Unit = model.Unit
            });

            return Ok(selectedNutrients);

        }
    }
}
