using System.Collections.Generic;

#nullable disable

namespace ProjektDieta.Models
{
    public partial class DietPlanForTemplate
    {
        public DietPlanForTemplate()
        {
            MealForTemplates = new HashSet<MealForTemplate>();
        }

        public long Id { get; set; }
        public long? SpecialistId { get; set; }
        public long? DemandsId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual SpecialistModel SpecialistModel { get; set; }
        public virtual ICollection<MealForTemplate> MealForTemplates { get; set; }
    }
}
