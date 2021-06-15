#nullable disable

using System.Collections.Generic;

namespace ProjektDieta.Models
{
    public partial class BaseRecipeModel : IModel
    {
        public BaseRecipeModel()
        {
            Products = new HashSet<BaseRecipeProductModel>();
        }
        public long Id { get; set; }
        public long? OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public int? Time { get; set; }
        public int? Portions { get; set; }

        public virtual ICollection<BaseRecipeProductModel> Products { get; set; }
    }
}
