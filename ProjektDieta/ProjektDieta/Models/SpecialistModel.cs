using System;
using System.Collections.Generic;

#nullable disable

namespace ProjektDieta.Models
{
    public partial class SpecialistModel : IModel
    {
        public SpecialistModel()
        {
            Collaborations = new HashSet<CollaborationModel>();
            DietPlanForTemplates = new HashSet<DietPlanForTemplate>();
            Invitations = new HashSet<InvitationModel>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; set; }
        public string Role { get; set; }

        public virtual ICollection<CollaborationModel> Collaborations { get; set; }
        public virtual ICollection<DietPlanForTemplate> DietPlanForTemplates { get; set; }
        public virtual ICollection<InvitationModel> Invitations { get; set; }
        public virtual ICollection<DemandTemplateModel> DemandTemplates { get; set; }
    }
}
