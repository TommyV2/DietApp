using System;

#nullable disable

namespace ProjektDieta.Models
{
    //TODO usunac demand?
    public partial class DietRecommendationModel : IModel
    {
        public long Id { get; set; }
        public long? CollaborationId { get; set; }
        public long? DemandsId { get; set; }
        public DateTime? SendDate { get; set; }
        public string Text { get; set; }

        public virtual CollaborationModel CollaborationModel { get; set; }
        public virtual DemandModel DemandsModel { get; set; }
    }
}
