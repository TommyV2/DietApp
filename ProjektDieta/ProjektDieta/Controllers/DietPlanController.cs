using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektDieta.Dtos;
using ProjektDieta.Models;
using ProjektDieta.Repository;
using Microsoft.Extensions.Logging;

namespace ProjektDieta.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DietPlanController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IRepository<ProductModel> _products;
        private readonly IRepository<NutrientModel> _nutrients;
        private readonly IRepository<ProductNutrientModel> _productNutrient;
        private readonly IRepository<BaseRecipeProductModel> _baseRecipeProduct;
        private readonly IRepository<BaseRecipeModel> _baseRecipes;
        private readonly IRepository<SpecialistModel> _specialist;
        private readonly IRepository<RecipeProductModel> _recipeProduct;
        private readonly IRepository<RecipeModel> _recipes;
        private readonly IRepository<MealModel> _meals;
        private readonly IRepository<DietPlanModel> _dietPlans;
        private readonly IRepository<CollaborationModel> _collaboration;
        private readonly IRepository<DemandModel> _demands;



        public DietPlanController(ILogger<ProductController> logger,
                                     IRepository<ProductModel> products,
                                     IRepository<NutrientModel> nutrients,
                                     IRepository<ProductNutrientModel> productNutrient,
                                     IRepository<BaseRecipeProductModel> baseRecipeProduct,
                                     IRepository<BaseRecipeModel> baseRecipes,
                                     IRepository<SpecialistModel> specialist,
                                     IRepository<RecipeProductModel> recipeProduct,
                                     IRepository<RecipeModel> recipes,
                                     IRepository<MealModel> meals,
                                     IRepository<DietPlanModel> dietPlans,
                                     IRepository<CollaborationModel> collaboration,
                                     IRepository<DemandModel> demands)
        {
            _logger = logger;
            _products = products;
            _nutrients = nutrients;
            _productNutrient = productNutrient;
            _baseRecipeProduct = baseRecipeProduct;
            _baseRecipes = baseRecipes;
            _specialist = specialist;
            _recipeProduct = recipeProduct;
            _recipes = recipes;
            _meals = meals;
            _dietPlans = dietPlans;
            _collaboration = collaboration;
            _demands = demands;
        }



        [HttpGet("mealnames")]
        public IActionResult GetMealNames()
        {
            List<string[]> names = new List<string[]>();
            string[] name0 = { };
            string[] name1 = { "Posiłek główny" };
            string[] name2 = { "Posiłek pierwszy", "Posiłek drugi" };
            string[] name3 = { "Śniadanie", "Obiad", "Kolacja" };
            string[] name4 = { "Śniadanie", "Drugie śniadanie", "Obiad", "Kolacja" };
            string[] name5 = { "Śniadanie", "Drugie śniadanie", "Obiad", "Podwieczorek", "Kolacja" };
            string[] name6 = { "Śniadanie", "Drugie śniadanie", "Lunch", "Obiad", "Podwieczorek", "Kolacja" };
            names.Add(name0);
            names.Add(name1);
            names.Add(name2);
            names.Add(name3);
            names.Add(name4);
            names.Add(name5);
            names.Add(name6);

            return Ok(names);
        }

        // GET: api/<DietPlanController>
        [HttpGet]
        public IActionResult Get(long colaboration_id)
        {
            List<long> dietPlans = new List<long>();
            var dps = _dietPlans.GetAll().Where(x => x.CollaborationId == colaboration_id);
            foreach (var dp in dps)
            {
                dietPlans.Add(dp.Id);
            }
            return Ok(dietPlans);
        }

        // GET api/<DietPlanController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<RecipeModel> allRecipes = _recipes.GetAll().ToList();
            List<MealDto> meals = new List<MealDto>();
            List<RecipeDto> recipes = new List<RecipeDto>();
            List<RecipeProductDto> products = new List<RecipeProductDto>();
            DietPlanModel dietPlan = _dietPlans.GetById(id, p => p.Meals);
            if (dietPlan == null)
                return NotFound($"Jadłospis o {id}, nie istnieje.");
            meals = dietPlan.Meals.Select(model => new MealDto()
            {
                Id = model.Id,
                Diet_Plan_Id = model.DietPlanId,
                Day = model.Day,
                Position = model.Position,
                Recipes = new List<RecipeDto>()
            }).ToList();
            foreach (var m in meals)
            {
                foreach (var r in allRecipes.Where(r => r.MealId == m.Id))
                {
                    RecipeModel recipeTmp = _recipes.GetById(r.Id, x => x.Products);
                    List<RecipeProductDto> productsTmp = new List<RecipeProductDto>();
                    double mass = 0;
                    int kcal = 0;
                    double carbohydrates = 0;
                    double sugar = 0;
                    double fat = 0;
                    double saturatedFat = 0;
                    double protein = 0;
                    double fiber = 0;

                    foreach (var pr in recipeTmp.Products)
                    {
                        var product = _products.GetById((long)pr.ProductId, x => x.ProductNutrients);
                        productsTmp.Add(new RecipeProductDto()
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
                            Amount = (int)pr.Amount
                        });
                        kcal += (int)((int)product.Kcal * ((double)pr.Amount / 100));
                        carbohydrates += (double)product.Carbohydrates * ((double)pr.Amount / 100);
                        sugar += (double)product.Sugar * ((double)pr.Amount / 100);
                        fat += (double)product.Fat * ((double)pr.Amount / 100);
                        saturatedFat += (double)product.SaturatedFat * ((double)pr.Amount / 100);
                        protein += (double)product.Protein * ((double)pr.Amount / 100);
                        fiber += (double)product.Fiber * ((double)pr.Amount / 100);
                        mass += (int)pr.Amount;
                    }

                    m.Recipes.Add(new RecipeDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Instruction = r.Instruction,
                        Time = r.Time,
                        Portions = r.Portions,
                        Portion = mass / (double)r.Portions,
                        Products = productsTmp,
                        Kcal = (int)(kcal / (mass / 100)),
                        Carbohydrates = carbohydrates / (mass / 100),
                        Sugar = sugar / (mass / 100),
                        Fat = fat / (mass / 100),
                        SaturatedFat = saturatedFat / (mass / 100),
                        Protein = protein / (mass / 100),
                        Fiber = fiber / (mass / 100),
                        Amount = (double)r.Amount
                    });
                }
            }

            DietPlanDto dietPlanDto = new DietPlanDto()
            {
                Id = dietPlan.Id,
                Colaboration_Id = (long)dietPlan.CollaborationId,
                Demands_Id = (long)dietPlan.DemandsId,
                Meals = meals
            };
            return Ok(dietPlanDto);
        }

        // POST api/<DietPlanController>
        [HttpPost]
        public IActionResult Post(long collaborationId, int days, int meals)
        {
            var collaboration = _collaboration.GetById(collaborationId);
            if (collaboration == null)
                return BadRequest("Podana współpraca nie istnieje.");
            var demands = _demands.Insert(new DemandModel()
            {
                Kcal = 2000,
                Carbohydrates = 300,
                Fat = 90,
                Protein = 90,
                Fibre = 20,
                Magnesium = 400,
                Calcium = 900,
                Iron = 10,
                Biotin = 10,
                Phosphorus = 10,
                Folate = 20,
                Cobalamin = 10,
                Zinc = 30,
                Copper = 30,
                Iodine = 30,
                Selenium = 10,
                Fluorine = 20,
                Sodium = 530,
                Potassium = 10,
                Chlorine = 10,
                Choline = 10,
                A = 30,
                D = 40,
                E = 50,
                K = 60,
                C = 70,
                Timine = 30,
                Riboflavin = 20,
                Niacin = 30,
                PantothenicAcid = 10,
                B6 = 10
            });

            DietPlanModel newDietPlan = new DietPlanModel()
            {
                CollaborationId = collaboration.Id,
                DemandsId = demands
            };

            _dietPlans.Insert(newDietPlan);

            if (days > 0 && days <= 30 && meals > 0 && meals <= 6)
            {
                for (int i = 1; i <= days; i++)
                {
                    for (int j = 1; j <= meals; j++)
                    {
                        _meals.Insert(new MealModel() { Day = i, Position = j, DietPlanId = newDietPlan.Id });
                    }
                }
            }

            return Ok(newDietPlan.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDietPlan(long id)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");
            _dietPlans.Delete(dietPlan);
            return Ok("Jadłospis został usuniety!");
        }

        //////////////////////////////////////////////////////


        [HttpGet("{id}/demands")]
        public IActionResult GetDemands(int id)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");

            var demands = _demands.GetById((long)dietPlan.DemandsId);

            DemandDto demandDto = new DemandDto()
            {
                Kcal = demands.Kcal,
                Carbohydrates = demands.Carbohydrates,
                Fat = demands.Fat,
                Protein = demands.Protein,
                Fibre = demands.Fibre,
                Magnesium = demands.Magnesium,
                Calcium = demands.Calcium,
                Iron = demands.Iron,
                Biotin = demands.Biotin,
                Phosphorus = demands.Phosphorus,
                Folate = demands.Folate,
                Cobalamin = demands.Cobalamin,
                Zinc = demands.Zinc,
                Copper = demands.Copper,
                Iodine = demands.Iodine,
                Selenium = demands.Selenium,
                Fluorine = demands.Fluorine,
                Sodium = demands.Sodium,
                Potassium = demands.Potassium,
                Chlorine = demands.Chlorine,
                Choline = demands.Choline,
                A = demands.A,
                D = demands.D,
                E = demands.E,
                K = demands.K,
                C = demands.C,
                Timine = demands.Timine,
                Riboflavin = demands.Riboflavin,
                Niacin = demands.Niacin,
                PantothenicAcid = demands.PantothenicAcid,
                B6 = demands.B6
            };


            return Ok(demandDto);
        }

        [HttpPut("{id}/demands")]
        public IActionResult PutDemands(int id, [FromBody] DemandDto demandDto)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");

            var demands = _demands.GetById((long)dietPlan.DemandsId);

            demands.Kcal = demandDto.Kcal;
            demands.Carbohydrates = demandDto.Carbohydrates;
            demands.Fat = demandDto.Fat;
            demands.Protein = demandDto.Protein;
            demands.Fibre = demandDto.Fibre;
            demands.Magnesium = demandDto.Magnesium;
            demands.Calcium = demandDto.Calcium;
            demands.Iron = demandDto.Iron;
            demands.Biotin = demandDto.Biotin;
            demands.Phosphorus = demandDto.Phosphorus;
            demands.Folate = demandDto.Folate;
            demands.Cobalamin = demandDto.Cobalamin;
            demands.Zinc = demandDto.Zinc;
            demands.Copper = demandDto.Copper;
            demands.Iodine = demandDto.Iodine;
            demands.Selenium = demandDto.Selenium;
            demands.Fluorine = demandDto.Fluorine;
            demands.Sodium = demandDto.Sodium;
            demands.Potassium = demandDto.Potassium;
            demands.Chlorine = demandDto.Chlorine;
            demands.Choline = demandDto.Choline;
            demands.A = demandDto.A;
            demands.D = demandDto.D;
            demands.E = demandDto.E;
            demands.K = demandDto.K;
            demands.C = demandDto.C;
            demands.Timine = demandDto.Timine;
            demands.Riboflavin = demandDto.Riboflavin;
            demands.Niacin = demandDto.Niacin;
            demands.PantothenicAcid = demandDto.PantothenicAcid;
            demands.B6 = demandDto.B6;

            _demands.Update(demands);

            return Ok("Zapotrzebowania zostały zmienione.");
        }

        /////////////////////////////////


        [HttpGet("{id}/meals")]
        public IActionResult GetMeals(int id)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");

            List<long> meals = new List<long>();
            var ms = _meals.GetAll().Where(x => x.DietPlanId == id);
            foreach (var m in ms)
            {
                meals.Add(m.Id);
            }
            return Ok(meals);
        }
        // GET <DietPlanController>/meal/{id}
        [HttpGet("{id}/meal/{meal_id}")]
        public IActionResult GetMeal(int id, long meal_id)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");
            var meal = _meals.GetById(meal_id);
            if (meal == null || meal.DietPlanId != id)
                return BadRequest("Posiłek nie istnieje!");

            List<RecipeModel> allRecipes = _recipes.GetAll().ToList();
            MealDto mealDto = new MealDto();
            List<RecipeDto> recipes = new List<RecipeDto>();
            List<RecipeProductDto> products = new List<RecipeProductDto>();

            mealDto = new MealDto()
            {
                Id = meal.Id,
                Diet_Plan_Id = meal.DietPlanId,
                Day = meal.Day,
                Position = meal.Position,
                Recipes = new List<RecipeDto>()
            };
            foreach (var r in allRecipes.Where(r => r.MealId == meal.Id))
            {
                RecipeModel recipeTmp = _recipes.GetById(r.Id, x => x.Products);
                List<RecipeProductDto> productsTmp = new List<RecipeProductDto>();
                double mass = 0;
                int kcal = 0;
                double carbohydrates = 0;
                double sugar = 0;
                double fat = 0;
                double saturatedFat = 0;
                double protein = 0;
                double fiber = 0;

                foreach (var pr in recipeTmp.Products)
                {
                    var product = _products.GetById((long)pr.ProductId, x => x.ProductNutrients);
                    productsTmp.Add(new RecipeProductDto()
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
                        Amount = (int)pr.Amount
                    });
                    kcal += (int)((int)product.Kcal * ((double)pr.Amount / 100));
                    carbohydrates += (double)product.Carbohydrates * ((double)pr.Amount / 100);
                    sugar += (double)product.Sugar * ((double)pr.Amount / 100);
                    fat += (double)product.Fat * ((double)pr.Amount / 100);
                    saturatedFat += (double)product.SaturatedFat * ((double)pr.Amount / 100);
                    protein += (double)product.Protein * ((double)pr.Amount / 100);
                    fiber += (double)product.Fiber * ((double)pr.Amount / 100);
                    mass += (int)pr.Amount;
                }

                mealDto.Recipes.Add(new RecipeDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Instruction = r.Instruction,
                    Time = r.Time,
                    Portions = r.Portions,
                    Portion = mass / (double)r.Portions,
                    Products = productsTmp,
                    Kcal = (int)(kcal / (mass / 100)),
                    Carbohydrates = carbohydrates / (mass / 100),
                    Sugar = sugar / (mass / 100),
                    Fat = fat / (mass / 100),
                    SaturatedFat = saturatedFat / (mass / 100),
                    Protein = protein / (mass / 100),
                    Fiber = fiber / (mass / 100),
                    Amount = (double)r.Amount
                });
            }

            return Ok(mealDto);
        }

        // POST
        [HttpPost("{id}/meal")]
        public IActionResult PostMeal(long id, int day)
        {
            var dietPlan = _dietPlans.GetById(id, x => x.Meals );
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");

            var meals = dietPlan.Meals.Where(x => x.Day == day);
            int pos = meals.Count() + 1;
            var meal_id = _meals.Insert(new MealModel()
            {
                Position = pos,
                Day = day,
                DietPlanId = dietPlan.Id
            });
            return Ok(meal_id);
        }
        // PUT
        [HttpPut("{id}/meal/{meal_id}")]
        public IActionResult PutMeal(int id, long meal_id, int day, int pos)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");
            var meal = _meals.GetById(meal_id);
            if (meal == null || meal.DietPlanId != id)
                return BadRequest("Posiłek nie istnieje!");
            meal.Day = day;
            meal.Position = pos;
            _meals.Update(meal);
            return Ok("Pozycja posiłku została zaktualizowana!");
        }

        [HttpDelete("{id}/meal/{meal_id}")]
        public IActionResult DeleteMeal(int id, long meal_id)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");
            var meal = _meals.GetById(meal_id);
            if (meal == null || meal.DietPlanId != id)
                return BadRequest("Posiłek nie istnieje!");
            _meals.Delete(meal);
            return Ok("Posiłek został usunięty!");
        }

        ///////////////////////////////////////


        // GET
        [HttpGet("{id}/meal/{meal_id}/recipe/{recipe_id}")]
        public IActionResult GetRecipe(int id,int meal_id, int recipe_id)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");
            var meal = _meals.GetById(meal_id);
            if (meal == null || meal.DietPlanId != id)
                return BadRequest("Posiłek nie istnieje!");
            var recipe = _recipes.GetById(recipe_id, x => x.Products);
            if (recipe == null || recipe.MealId != meal_id)
                return BadRequest("Przepis nie istnieje!");

            List<RecipeProductDto> products = new List<RecipeProductDto>();
            double mass = 0;
            int kcal = 0;
            double carbohydrates = 0;
            double sugar = 0;
            double fat = 0;
            double saturatedFat = 0;
            double protein = 0;
            double fiber = 0;

            foreach (var pr in recipe.Products)
            {
                var product = _products.GetById((long)pr.ProductId, x => x.ProductNutrients);
                products.Add(new RecipeProductDto()
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
                    Amount = (int)pr.Amount
                });
                kcal += (int)((int)product.Kcal * ((double)pr.Amount / 100));
                carbohydrates += (double)product.Carbohydrates * ((double)pr.Amount / 100);
                sugar += (double)product.Sugar * ((double)pr.Amount / 100);
                fat += (double)product.Fat * ((double)pr.Amount / 100);
                saturatedFat += (double)product.SaturatedFat * ((double)pr.Amount / 100);
                protein += (double)product.Protein * ((double)pr.Amount / 100);
                fiber += (double)product.Fiber * ((double)pr.Amount / 100);
                mass += (int)pr.Amount;
            }

            var recipeDto=new RecipeDto()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Instruction = recipe.Instruction,
                Time = recipe.Time,
                Portions = recipe.Portions,
                Portion = mass / (double)recipe.Portions,
                Products = products,
                Kcal = (int)(kcal / (mass / 100)),
                Carbohydrates = carbohydrates / (mass / 100),
                Sugar = sugar / (mass / 100),
                Fat = fat / (mass / 100),
                SaturatedFat = saturatedFat / (mass / 100),
                Protein = protein / (mass / 100),
                Fiber = fiber / (mass / 100),
                Amount = (double)recipe.Amount
            };
            return Ok(recipeDto);
        }

        // POST
        [HttpPost("{id}/meal/{meal_id}/recipe")]
        public IActionResult Postrecipe([FromBody] RecipeDto newRecipe, int id, int meal_id)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");
            var meal = _meals.GetById(meal_id);
            if (meal == null || meal.DietPlanId != id)
                return BadRequest("Posiłek nie istnieje!");

            RecipeModel recipe = new RecipeModel()
            {
                MealId = meal_id,
                Name = newRecipe.Name,
                Instruction = newRecipe.Instruction,
                Time = newRecipe.Time,
                Portions = newRecipe.Portions,
                Amount = newRecipe.Amount
            };
            _recipes.Insert(recipe);

            foreach (var pr in newRecipe.Products)
            {
                _recipeProduct.Insert(new RecipeProductModel()
                {
                    RecipeId = recipe.Id,
                    ProductId = pr.Id,
                    Amount = pr.Amount
                });
            }
            return Ok(recipe.Id);
        }

        // PUT
        [HttpPut("{id}/meal/{meal_id}/recipe/{recipe_id}")]
        public IActionResult PutRecipe([FromBody] RecipeDto newRecipe, int id, int meal_id, int recipe_id)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");
            var meal = _meals.GetById(meal_id);
            if (meal == null || meal.DietPlanId != id)
                return BadRequest("Posiłek nie istnieje!");
            var recipe = _recipes.GetById(recipe_id, x => x.Products);
            if (recipe == null || recipe.MealId != meal_id)
                return BadRequest("Przepis nie istnieje!");

            recipe.Name = newRecipe.Name;
            recipe.Instruction = newRecipe.Instruction;
            recipe.Time = newRecipe.Time;
            recipe.Portions = newRecipe.Portions;
            recipe.Amount = newRecipe.Amount;
            _recipes.Update(recipe);

            var productList = recipe.Products.ToList();
            foreach (var p in productList)
            {
                _recipeProduct.Delete(p);
            }

            foreach (var pr in newRecipe.Products)
            {
                _recipeProduct.Insert(new RecipeProductModel()
                {
                    RecipeId = recipe.Id,
                    ProductId = pr.Id,
                    Amount = pr.Amount
                });
            }
            return Ok("Przepis został zaktualizowany!");
        }

        // DELETE
        [HttpDelete("{id}/meal/{meal_id}/recipe/{recipe_id}")]
        public IActionResult DeleteRecipe(int id, int meal_id, int recipe_id)
        {
            var dietPlan = _dietPlans.GetById(id);
            if (dietPlan == null)
                return BadRequest("Jadłospis nie istnieje!");
            var meal = _meals.GetById(meal_id);
            if (meal == null || meal.DietPlanId != id)
                return BadRequest("Posiłek nie istnieje!");
            var recipe = _recipes.GetById(recipe_id, x => x.Products);
            if (recipe == null || recipe.MealId != meal_id)
                return BadRequest("Przepis nie istnieje!");
            var productList = recipe.Products.ToList();
            foreach (var p in productList)
            {
                _recipeProduct.Delete(p);
            }
            _recipes.Delete(recipe);
            return Ok("Przepis usunięty!");
        }


    }
}
