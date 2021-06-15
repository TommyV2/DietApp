using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Models
{
    public class InvitationModel : IModel
    {
        public long Id { get; set; }
        public long? SpecialistId { get; set; }
        public long? CustomerId { get; set; }
        public DateTime? SendDate { get; set; }
        public string Type { get; set; }
        public string InvitedBy { get; set; }

        public virtual CustomerModel Customer { get; set; }
        public virtual SpecialistModel Specialist { get; set; }
    }
}
