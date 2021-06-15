using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Dtos
{
    public class LoginResponseDto
    {
        public long? CustomerId { get; set; }
        public long? SpecialistId { get; set; }
    }
}
