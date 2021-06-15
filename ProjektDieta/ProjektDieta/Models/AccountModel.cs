using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Models
{
    public class AccountModel : IModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long? SpecialistId { get; set; }
        public long? CustomerId { get; set; }
    }
}
