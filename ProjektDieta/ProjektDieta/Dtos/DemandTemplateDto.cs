using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektDieta.SwaggerUtils;

namespace ProjektDieta.Dtos
{
    public class DemandTemplateDto
    {
        [SwaggerIgnoreProperty]
        public long? Id { get; set; }
        public long? SpecialistId { get; set; }
        public int? Kcal { get; set; }
        public int? Carbohydrates { get; set; }
        public int? Fat { get; set; }
        public int? Protein { get; set; }
        public double? Fibre { get; set; }
        public double? Magnesium { get; set; }
        public double? Calcium { get; set; }
        public double? Iron { get; set; }
        public double? Biotin { get; set; }
        public double? Phosphorus { get; set; }
        public double? Folate { get; set; }
        public double? Cobalamin { get; set; }
        public double? Zinc { get; set; }
        public double? Copper { get; set; }
        public double? Iodine { get; set; }
        public double? Selenium { get; set; }
        public double? Fluorine { get; set; }
        public double? Sodium { get; set; }
        public double? Potassium { get; set; }
        public double? Chlorine { get; set; }
        public double? Choline { get; set; }
        public double? A { get; set; }
        public double? D { get; set; }
        public double? E { get; set; }
        public double? K { get; set; }
        public double? C { get; set; }
        public double? Timine { get; set; }
        public double? Riboflavin { get; set; }
        public double? Niacin { get; set; }
        public double? PantothenicAcid { get; set; }
        public double? B6 { get; set; }
        public string Name { get; set; }
    }
}
