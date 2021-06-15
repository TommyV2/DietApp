using System.Collections.Generic;

#nullable disable

namespace ProjektDieta.Models
{
    public partial class CollaborationModel : IModel
    {
        public CollaborationModel()
        {
            DietPlans = new HashSet<DietPlanModel>();
            DietRecommendations = new HashSet<DietRecommendationModel>();
        }

        public long Id { get; set; }
        public long? SpecialistId { get; set; }
        public long? CustomerId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }

        public virtual CustomerModel Customer { get; set; }
        public virtual SpecialistModel Specialist { get; set; }
        public virtual ICollection<DietPlanModel> DietPlans { get; set; }
        public virtual ICollection<DietRecommendationModel> DietRecommendations { get; set; }
    }
}
