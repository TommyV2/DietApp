#nullable disable

using System.Collections.Generic;

namespace ProjektDieta.Models
{
    public partial class MealModel : IModel
    {
        public long Id { get; set; }
        public long? DietPlanId { get; set; }
        public int? Day { get; set; }
        public int? Position { get; set; }

        public virtual DietPlanModel DietPlan { get; set; }
        public virtual ICollection<RecipeModel> Recipes { get; set; }
    }
}
