using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Dtos
{
    public class NutrientDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PolName { get; set; }
        public string Unit { get; set; }
        public string ShortName { get; set; }
    }
}
