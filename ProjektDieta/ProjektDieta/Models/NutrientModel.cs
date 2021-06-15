#nullable disable

namespace ProjektDieta.Models
{
    public partial class NutrientModel : IModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PolName { get; set; }
        public string Unit { get; set; }
        public string ShortName { get; set; }
    }
}
