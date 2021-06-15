using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektDieta.SwaggerUtils;

namespace ProjektDieta.Dtos
{
    public class CollaborationDto
    {
        [SwaggerIgnoreProperty]
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public long? SpecialistId { get; set; }
        [SwaggerIgnoreProperty]
        public string SpecialistName { get; set; }
        [SwaggerIgnoreProperty]
        public string CustomerName { get; set; }
        
        public string Type { get; set; }

        [SwaggerIgnoreProperty]
        public string Status { get; set; }
    }
}
