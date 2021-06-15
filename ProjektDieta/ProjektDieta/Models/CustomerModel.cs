using System;
using System.Collections.Generic;

#nullable disable

namespace ProjektDieta.Models
{
    public partial class CustomerModel : IModel
    {
        public CustomerModel()
        {
            BodyMeasurements = new HashSet<BodyMeasurement>();
            Collaborations = new HashSet<CollaborationModel>();
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

        public virtual ICollection<BodyMeasurement> BodyMeasurements { get; set; }
        public virtual ICollection<CollaborationModel> Collaborations { get; set; }
        public virtual ICollection<InvitationModel> Invitations { get; set; }
}
}
