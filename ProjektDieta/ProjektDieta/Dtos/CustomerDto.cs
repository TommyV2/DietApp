using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ProjektDieta.Enums;
using ProjektDieta.SwaggerUtils;

namespace ProjektDieta.Dtos
{
    public class CustomerDto
    {
        
        [SwaggerIgnoreProperty]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime? Birthdate { get; set; }

        public string City { get; set; }

        public string Gender { get; set; }



        //TODO
        // photo
    }
}
