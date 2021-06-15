using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjektDieta.Dtos;
using ProjektDieta.Models;
using ProjektDieta.Repository;
using ProjektDieta.Utils;
using System;
using ProjektDieta.Enums;
using System.Collections.Generic;
using System.Linq;
using ProjektDieta.Extensions;
using System.Threading.Tasks;

namespace ProjektDieta.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseRecipeController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IRepository<ProductModel> _products;
        private readonly IRepository<NutrientModel> _nutrients;
        private readonly IRepository<ProductNutrientModel> _productNutrient;
        private readonly IRepository<BaseRecipeProductModel> _baseRecipeProduct;
        private readonly IRepository<BaseRecipeModel> _recipes;
        private readonly IRepository<SpecialistModel> _specialist;

        public BaseRecipeController(ILogger<ProductController> logger,
                                     IRepository<ProductModel> products,
                                     IRepository<NutrientModel> nutrients,
                                     IRepository<ProductNutrientModel> productNutrient,
                                     IRepository<BaseRecipeProductModel> baseRecipeProduct,
                                     IRepository<BaseRecipeModel> recipes,
                                     IRepository<SpecialistModel> specialist)
        {
            _logger = logger;
            _products = products;
            _nutrients = nutrients;
            _productNutrient = productNutrient;
            _baseRecipeProduct = baseRecipeProduct;
            _recipes = recipes;
            _specialist = specialist;
        }

        [HttpGet]
        public IActionResult Get(string name)//Dodać info o odżywczych
        {
            var recipes = _recipes.GetAll();
            var filtered = name == null
                ? recipes
                : recipes.Where(model => model.Name.Contains(name));
            var selectedRecipes = filtered.Select(model => new BaseRecipeDto()
            {
                Id = model.Id,
                OwnerId = model.OwnerId,
                OwnerName = model.OwnerName,
                Name = model.Name,
                Instruction = model.Instruction,
                Time = model.Time,
                Portions = model.Portions,
                Portion = 0,
                Products = null,
                Kcal = 0,
                Carbohydrates = 0,
                Sugar = 0,
                Fat = 0,
                SaturatedFat = 0,
                Protein = 0,
                Fiber = 0

            });
            selectedRecipes = selectedRecipes.ToList();
            var productsLists = _baseRecipeProduct.GetAll();
            var allProducts = _products.GetAll().ToList();
            foreach (var recipe in selectedRecipes)
            {
                var procuctList = productsLists.Where(x => x.RecipeId == recipe.Id);

                List<RecipeProductDto> products = new List<RecipeProductDto>();
                double mass = 0;
                int kcal = 0;
                double carbohydrates = 0;
                double sugar = 0;
                double fat = 0;
                double saturatedFat = 0;
                double protein = 0;
                double fiber = 0;
               
                foreach (BaseRecipeProductModel brpm in procuctList)
                {

                    var p = allProducts.Find(p => p.Id == brpm.ProductId);
                    products.Add(new RecipeProductDto()
                    {
                        Id = p.Id,
                        OwnerId = p.OwnerId,
                        Source = p.Source,
                        Name = p.Name,
                        Kcal = p.Kcal,
                        Carbohydrates = p.Carbohydrates,
                        Sugar = p.Sugar,
                        Fat = p.Fat,
                        SaturatedFat = p.SaturatedFat,
                        Protein = p.Protein,
                        Fiber = p.Fiber,
                        Amount = (int)brpm.Amount
                    });
                    kcal += (int)((int)p.Kcal * ((double)brpm.Amount / 100));
                    carbohydrates += (double)p.Carbohydrates * ((double)brpm.Amount / 100);
                    sugar += (double)p.Sugar * ((double)brpm.Amount / 100);
                    fat += (double)p.Fat * ((double)brpm.Amount / 100);
                    saturatedFat += (double)p.SaturatedFat * ((double)brpm.Amount / 100);
                    protein += (double)p.Protein * ((double)brpm.Amount / 100);
                    fiber += (double)p.Fiber * ((double)brpm.Amount / 100);
                    mass += (int)brpm.Amount;
                    
                }
                recipe.Portion = mass / (double)recipe.Portions;
                recipe.Products = products;
                recipe.Kcal = (int)(kcal / (mass / 100));
                recipe.Carbohydrates = carbohydrates / (mass / 100);
                recipe.Sugar = sugar / (mass / 100);
                recipe.Fat = fat / (mass / 100);
                recipe.SaturatedFat = saturatedFat / (mass / 100);
                recipe.Protein = protein / (mass / 100);
                recipe.Fiber = fiber / (mass / 100);

            }
            return Ok(selectedRecipes);
        }

        [HttpGet]
        [Route("by-owner")]
        public IActionResult Get(long? id)//Dodać info o odżywczych
        {
            var recipes = _recipes.GetAll();
            var filtered = id == null
                ? recipes
                : recipes.Where(model => model.OwnerId == id);
            var selectedRecipes = filtered.Select(model => new BaseRecipeDto()
            {
                Id = model.Id,
                OwnerId = model.OwnerId,
                OwnerName = model.OwnerName,
                Name = model.Name,
                Instruction = model.Instruction,
                Time = model.Time,
                Portions = model.Portions,
                Portion = 0,
                Products = null,
                Kcal = 0,
                Carbohydrates = 0,
                Sugar = 0,
                Fat = 0,
                SaturatedFat = 0,
                Protein = 0,
                Fiber = 0

            });
            selectedRecipes = selectedRecipes.ToList();
            var productsLists = _baseRecipeProduct.GetAll();
            var allProducts = _products.GetAll().ToList();
            foreach (var recipe in selectedRecipes)
            {
                var procuctList = productsLists.Where(x => x.RecipeId == recipe.Id);

                List<RecipeProductDto> products = new List<RecipeProductDto>();
                double mass = 0;
                int kcal = 0;
                double carbohydrates = 0;
                double sugar = 0;
                double fat = 0;
                double saturatedFat = 0;
                double protein = 0;
                double fiber = 0;

                foreach (BaseRecipeProductModel brpm in procuctList)
                {

                    var p = allProducts.Find(p => p.Id == brpm.ProductId);
                    products.Add(new RecipeProductDto()
                    {
                        Id = p.Id,
                        OwnerId = p.OwnerId,
                        Source = p.Source,
                        Name = p.Name,
                        Kcal = p.Kcal,
                        Carbohydrates = p.Carbohydrates,
                        Sugar = p.Sugar,
                        Fat = p.Fat,
                        SaturatedFat = p.SaturatedFat,
                        Protein = p.Protein,
                        Fiber = p.Fiber,
                        Amount = (int)brpm.Amount
                    });
                    kcal += (int)((int)p.Kcal * ((double)brpm.Amount / 100));
                    carbohydrates += (double)p.Carbohydrates * ((double)brpm.Amount / 100);
                    sugar += (double)p.Sugar * ((double)brpm.Amount / 100);
                    fat += (double)p.Fat * ((double)brpm.Amount / 100);
                    saturatedFat += (double)p.SaturatedFat * ((double)brpm.Amount / 100);
                    protein += (double)p.Protein * ((double)brpm.Amount / 100);
                    fiber += (double)p.Fiber * ((double)brpm.Amount / 100);
                    mass += (int)brpm.Amount;

                }
                recipe.Portion = mass / (double)recipe.Portions;
                recipe.Products = products;
                recipe.Kcal = (int)(kcal / (mass / 100));
                recipe.Carbohydrates = carbohydrates / (mass / 100);
                recipe.Sugar = sugar / (mass / 100);
                recipe.Fat = fat / (mass / 100);
                recipe.SaturatedFat = saturatedFat / (mass / 100);
                recipe.Protein = protein / (mass / 100);
                recipe.Fiber = fiber / (mass / 100);

            }
            return Ok(selectedRecipes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var recipe = _recipes.GetById(id, p => p.Products);
            if (recipe == null)
                return BadRequest();

            Dictionary<long?, double> recipeNutrients = new Dictionary<long?, double>();
            List<RecipeProductDto> products = new List<RecipeProductDto>();
            double mass = 0;
            int kcal = 0;
            double carbohydrates = 0;
            double sugar = 0;
            double fat = 0;
            double saturatedFat = 0;
            double protein = 0;
            double fiber = 0;
            foreach (BaseRecipeProductModel brpm in recipe.Products)
            {
                var p = _products.GetById((long)brpm.ProductId, p => p.ProductNutrients);
                products.Add(new RecipeProductDto()
                {
                    Id = p.Id,
                    OwnerId = p.OwnerId,
                    Source = p.Source,
                    Name = p.Name,
                    Kcal = p.Kcal,
                    Carbohydrates = p.Carbohydrates,
                    Sugar = p.Sugar,
                    Fat = p.Fat,
                    SaturatedFat = p.SaturatedFat,
                    Protein = p.Protein,
                    Fiber = p.Fiber,
                    Amount = (int)brpm.Amount
                });
                kcal += (int)((int)p.Kcal *((double)brpm.Amount/100));
                carbohydrates += (double)p.Carbohydrates * ((double)brpm.Amount / 100);
                sugar += (double)p.Sugar * ((double)brpm.Amount / 100);
                fat += (double)p.Fat * ((double)brpm.Amount / 100);
                saturatedFat += (double)p.SaturatedFat * ((double)brpm.Amount / 100);
                protein += (double)p.Protein * ((double)brpm.Amount / 100);
                fiber += (double)p.Fiber * ((double)brpm.Amount / 100);
                foreach (ProductNutrientModel pnm in p.ProductNutrients)
                {
                    if (!recipeNutrients.ContainsKey(pnm.NutrientId))
                        recipeNutrients.Add(pnm.NutrientId, (double)(brpm.Amount/100) * pnm.Amount);
                    else
                    {
                        double tmp;
                        recipeNutrients.Remove(pnm.NutrientId, out tmp);
                        recipeNutrients.Add(pnm.NutrientId, tmp + (double)(brpm.Amount / 100) * pnm.Amount);
                    }
                    
                }
                mass += (int)brpm.Amount;
            }
            var recipeDto = new BaseRecipeDto()
            {
                Id = recipe.Id,
                OwnerId = recipe.OwnerId,
                OwnerName = recipe.OwnerName,
                Name = recipe.Name,
                Instruction = recipe.Instruction,
                Time = recipe.Time,
                Portions = recipe.Portions,
                Portion = mass/(double)recipe.Portions,
                Products = products,
                Kcal = (int)(kcal/ (mass/100)),
                Carbohydrates = carbohydrates / (mass / 100),
                Sugar = sugar / (mass / 100),
                Fat = fat / (mass / 100),
                SaturatedFat = saturatedFat / (mass / 100),
                Protein = protein / (mass / 100),
                Fiber = fiber / (mass / 100)
            };
            return Ok(recipeDto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewRecipeDto newRecipe)
        {
            long owner;
            string ownerFromSession = HttpContext.Session.GetString(SessionKeys.SpecialistId);
            long.TryParse(ownerFromSession, out owner);
            BaseRecipeModel baseRecipe = new BaseRecipeModel()
            {
                OwnerId = owner, // tu pakujemy ownera z sesji
                OwnerName = "Właściciel", // tu pobieramby imie i  nazwisko z bazy
                Name = newRecipe.Name,
                Instruction = newRecipe.Instruction,
                Time = newRecipe.Time,
                Portions = newRecipe.Portions
            };
            _recipes.Insert(baseRecipe);

            foreach(ProductRecipeDto pr in newRecipe.Products)
            {
                _baseRecipeProduct.Insert(new BaseRecipeProductModel() 
                                            { 
                                                RecipeId = baseRecipe.Id,
                                                ProductId = pr.ProductId,
                                                Amount = pr.Amount,
                                            });
            }
            return Ok(baseRecipe.Id);
        }
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] NewRecipeDto newRecipe)
        {
            //Sprawdź uprawnienia
            var recipe = _recipes.GetById(id, p => p.Products);
            if (recipe == null) return BadRequest("Nie ma takiego przepisu!");

            recipe.Name = newRecipe.Name;
            recipe.Instruction = newRecipe.Instruction;
            recipe.Time = newRecipe.Time;
            recipe.Portions = newRecipe.Portions;

            foreach (BaseRecipeProductModel brpm in recipe.Products)
            {
                _baseRecipeProduct.Delete(_baseRecipeProduct.GetById(brpm.Id));
            }
            recipe.Products = null;
            _recipes.Update(recipe);

            foreach (ProductRecipeDto pr in newRecipe.Products)
            {
                _baseRecipeProduct.Insert(new BaseRecipeProductModel()
                {
                    RecipeId = recipe.Id,
                    ProductId = pr.ProductId,
                    Amount = pr.Amount,
                });
            }
            

            return Ok("Pomyślnie zaktualizowano przepis!");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var recipe = _recipes.GetById(id);
            if (recipe == null) return BadRequest("Nie ma takiego przepisu!");

            _recipes.Delete(recipe);
            return Ok("Pomyślnie usunięto!");
        }
    }
}
