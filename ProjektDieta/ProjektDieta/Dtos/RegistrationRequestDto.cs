using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjektDieta.Dtos
{
    public class RegistrationRequestDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccountRole { get; set; }

    }
}
