using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektDieta.SwaggerUtils;

namespace ProjektDieta.Dtos
{
    public class DietRecommendationDto
    {
        [SwaggerIgnoreProperty]
        public long Id { get; set; }
        public long? CollaborationId { get; set; }
        //public long? DemandTemplateId { get; set; }
        public DateTime? SendDate { get; set; }
        public string Text { get; set; }
    }
}
