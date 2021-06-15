#nullable disable

using System.Collections.Generic;

namespace ProjektDieta.Models
{
    public partial class RecipeModel : IModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public int? Time { get; set; }
        public int? Portions { get; set; }
        public double? Amount { get; set; }
        public long? MealId { get; set; }

        public virtual MealModel Meal { get; set; }

        public virtual ICollection<RecipeProductModel> Products { get; set; }
    }
}
