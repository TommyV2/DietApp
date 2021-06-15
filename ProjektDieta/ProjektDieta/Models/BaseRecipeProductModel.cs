#nullable disable

namespace ProjektDieta.Models
{
    public partial class BaseRecipeProductModel : IModel
    {
        public long Id { get; set; }
        public long? RecipeId { get; set; }
        public long? ProductId { get; set; }
        public int? Amount { get; set; }

        public virtual ProductModel Product { get; set; }
        public virtual BaseRecipeModel Recipe { get; set; }
    }
}
