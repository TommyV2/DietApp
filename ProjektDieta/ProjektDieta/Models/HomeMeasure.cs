#nullable disable

namespace ProjektDieta.Models
{
    public partial class HomeMeasure
    {
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public int? Mass { get; set; }

        public virtual ProductModel Product { get; set; }
    }
}
