#nullable disable

namespace ProjektDieta.Models
{
    public partial class FoodPreference
    {
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public string Product { get; set; }
        public string RelationType { get; set; }

        public virtual CustomerModel CustomerModel { get; set; }
    }
}
