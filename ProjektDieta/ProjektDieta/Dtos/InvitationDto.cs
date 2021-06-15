using ProjektDieta.SwaggerUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Dtos
{
    public class InvitationDto
    {
        [SwaggerIgnoreProperty]
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public long? SpecialistId { get; set; }
        [SwaggerIgnoreProperty]
        public string SpecialistName { get; set; }
        [SwaggerIgnoreProperty]
        public string CustomerName { get; set; }
        [SwaggerIgnoreProperty]
        public DateTime? SendDate { get; set; }
        public string Type { get; set; }    
        public string InvitedBy { get; set; }//tylko tymczasowo zanim będzie można idetyfikwoac użytkowników
    }
}
