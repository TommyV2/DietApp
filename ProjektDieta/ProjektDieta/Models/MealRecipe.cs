#nullable disable

namespace ProjektDieta.Models
{
    public partial class MealRecipe
    {
        public long? MealId { get; set; }
        public long? RecipeId { get; set; }

        public virtual MealModel Meal { get; set; }
        public virtual RecipeModel Recipe { get; set; }
    }
}
