using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Dtos
{
    public class RecipeProductDto
    {
        public long Id { get; set; }
        public long? OwnerId { get; set; }
        public string? Source { get; set; }
        public string? Name { get; set; }
        public int? Kcal { get; set; }
        public double? Carbohydrates { get; set; }
        public double? Sugar { get; set; }
        public double? Fat { get; set; }
        public double? SaturatedFat { get; set; }
        public double? Protein { get; set; }
        public double? Fiber { get; set; }
        //public ICollection<ProductNutrientDto> Nutrients { get; set; }
        public int Amount { get; set; }
    }
}
