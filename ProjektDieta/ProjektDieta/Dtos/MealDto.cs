using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Dtos
{
    public class MealDto
    {
        public long Id { get; set; }
        public long? Diet_Plan_Id { get; set; }
        public int? Day { get; set; }
        public int? Position { get; set; }
        public ICollection<RecipeDto> Recipes { get; set; }
    }
}
