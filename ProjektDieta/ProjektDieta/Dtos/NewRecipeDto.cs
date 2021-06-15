using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Dtos
{
    public class NewRecipeDto
    {
        public string Name { get; set; }
        public string Instruction { get; set; }
        public int? Time { get; set; }
        public int? Portions { get; set; }
        public ICollection<ProductRecipeDto> Products { get; set; }
    }
}
