using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Dtos
{
    public class BaseRecipeDto
    {
        public long Id { get; set; }
        public long? OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public int? Time { get; set; }
        public int? Portions { get; set; }
        public double Portion { get; set; }
        public int? Kcal { get; set; }
        public double? Carbohydrates { get; set; }
        public double? Sugar { get; set; }
        public double? Fat { get; set; }
        public double? SaturatedFat { get; set; }
        public double? Protein { get; set; }
        public double? Fiber { get; set; }
        public ICollection<RecipeProductDto> Products { get; set; }
    }
}
