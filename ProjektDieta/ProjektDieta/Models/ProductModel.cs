#nullable disable

using System.Collections.Generic;

namespace ProjektDieta.Models
{ 
    public partial class ProductModel : IModel
    {
        public ProductModel()
        {
            ProductNutrients = new HashSet<ProductNutrientModel>();
        }

        public long Id { get; set; }
        public long? OwnerId { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }
        public int? Kcal { get; set; }
        public double? Carbohydrates { get; set; }
        public double? Sugar { get; set; }
        public double? Fat { get; set; }
        public double? SaturatedFat { get; set; }
        public double? Protein { get; set; }
        public double? Fiber { get; set; }

        public virtual ICollection<ProductNutrientModel> ProductNutrients { get; set; }
    }
}
