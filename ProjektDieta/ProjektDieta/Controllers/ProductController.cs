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
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IRepository<ProductModel> _products;
        private readonly IRepository<NutrientModel> _nutrients;
        private readonly IRepository<ProductNutrientModel> _productNutrient;


        public ProductController(ILogger<ProductController> logger,
                                     IRepository<ProductModel> products,
                                     IRepository<NutrientModel> nutrients,
                                     IRepository<ProductNutrientModel> productNutrient)
        {
            _logger = logger;
            _products = products;
            _nutrients = nutrients;
            _productNutrient = productNutrient;
        }
        // GET
        [HttpGet]
        public IActionResult Get(string name)
        {
            var products = _products.GetAll();
            var filtered = name == null
                ? products
                : products.Where(model => model.Name.Contains(name));
            var selectedProducts = filtered.Select(model => new ProductDto()
            {
                Id = model.Id,
                OwnerId = model.OwnerId,
                Source = model.Source,
                Name = model.Name,
                Kcal = model.Kcal,
                Carbohydrates = model.Carbohydrates,
                Sugar = model.Sugar,
                Fat = model.Fat,
                SaturatedFat = model.SaturatedFat,
                Protein = model.Protein,
                Fiber = model.Fiber
            });

            return Ok(selectedProducts);

        }

        // GET
        [HttpGet]
        [Route("by-owner")]
        public IActionResult Get(long? specialistId)
        {
            var products = _products.GetAll();
            var filtered = specialistId == null
                ? products
                : products.Where(model => model.OwnerId == specialistId);
            var selectedProducts = filtered.Select(model => new ProductDto()
            {
                Id = model.Id,
                OwnerId = model.OwnerId,
                Source = model.Source,
                Name = model.Name,
                Kcal = model.Kcal,
                Carbohydrates = model.Carbohydrates,
                Sugar = model.Sugar,
                Fat = model.Fat,
                SaturatedFat = model.SaturatedFat,
                Protein = model.Protein,
                Fiber = model.Fiber
            });

            return Ok(selectedProducts);

        }

        // GET
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _products.GetById(id);
            if (product == null)
                return BadRequest();

            List<ProductNutrientDto> nut = new List<ProductNutrientDto>();
            var pn = _productNutrient.GetAll().Where(m => m.ProductId == product.Id);
            foreach (ProductNutrientModel pnm in pn)
            {
                nut.Add(new ProductNutrientDto(){ NutrientId = pnm.NutrientId, Amount = pnm.Amount });
            }

            var productDto = new ProductDto()
            {
                Id = product.Id,
                OwnerId = product.OwnerId,
                Source = product.Source,
                Name = product.Name,
                Kcal = product.Kcal,
                Carbohydrates = product.Carbohydrates,
                Sugar = product.Sugar,
                Fat = product.Fat,
                SaturatedFat = product.SaturatedFat,
                Protein = product.Protein,
                Fiber = product.Fiber,
                Nutrients = nut
            };
            return Ok(productDto);
        }

        // POST
        [HttpPost]
        public IActionResult Post([FromBody] ProductDto newProduct)
        {
            var product = new ProductModel()
            {
                OwnerId = newProduct.OwnerId,
                Source = newProduct.Source,
                Name = newProduct.Name,
                Kcal = newProduct.Kcal,
                Carbohydrates = newProduct.Carbohydrates,
                Sugar = newProduct.Sugar,
                Fat = newProduct.Fat,
                SaturatedFat = newProduct.SaturatedFat,
                Protein = newProduct.Protein,
                Fiber = newProduct.Fiber
            };
            _products.Insert(product);
            foreach(ProductNutrientDto pn in newProduct.Nutrients)
            {
                _productNutrient.Insert(new ProductNutrientModel()
                {
                    NutrientId = pn.NutrientId,
                    ProductId = product.Id,
                    Amount = pn.Amount
                });
            }

            return Ok(product.Id);
        }

        // PUT
        /*[HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return BadRequest();
        }*/

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.GetById(id);
            _products.Delete(product);
            return Ok("Product deleted!");
        }
    }
}
