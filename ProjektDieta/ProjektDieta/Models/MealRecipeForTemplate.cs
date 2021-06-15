#nullable disable

namespace ProjektDieta.Models
{
    public partial class MealRecipeForTemplate
    {
        public long? MealId { get; set; }
        public long? RecipeId { get; set; }

        public virtual MealForTemplate Meal { get; set; }
        public virtual RecipeModel Recipe { get; set; }
    }
}
