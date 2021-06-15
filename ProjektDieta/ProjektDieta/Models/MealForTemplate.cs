#nullable disable

namespace ProjektDieta.Models
{
    public partial class MealForTemplate
    {
        public long Id { get; set; }
        public long? DietPlanId { get; set; }
        public int? Day { get; set; }
        public int? Position { get; set; }

        public virtual DietPlanForTemplate DietPlan { get; set; }
    }
}
