using System;
using System.Collections.Generic;

#nullable disable

namespace ProjektDieta.Models
{
    public partial class DietPlanModel : IModel
    {
        public DietPlanModel()
        {
            Meals = new HashSet<MealModel>();
        }

        public long Id { get; set; }
        public long? CollaborationId { get; set; }
        public long? DemandsId { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual CollaborationModel CollaborationModel { get; set; }
        public virtual DemandModel DemandsModel { get; set; }
        public virtual ICollection<MealModel> Meals { get; set; }
    }
}
