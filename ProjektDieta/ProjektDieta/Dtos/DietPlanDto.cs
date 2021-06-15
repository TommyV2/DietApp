using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDieta.Dtos
{
    public class DietPlanDto
    {
        public long Id { get; set; }
        public long Colaboration_Id { get; set; }
        public long Demands_Id { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? StartDate { get; set; }
        public ICollection<MealDto> Meals { get; set; }
    }
}
