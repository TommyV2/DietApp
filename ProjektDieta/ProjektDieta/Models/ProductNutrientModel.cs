#nullable disable

namespace ProjektDieta.Models
{
    public partial class ProductNutrientModel : IModel
    {
        public long Id { get; set; }
        public long? NutrientId { get; set; }
        public long? ProductId { get; set; }
        public double Amount { get; set; }

        public virtual NutrientModel Nutrient { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}
