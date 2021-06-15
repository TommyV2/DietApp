using System;

#nullable disable

namespace ProjektDieta.Models
{
    public partial class BodyMeasurement
    {
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public double? Neck { get; set; }
        public double? Chest { get; set; }
        public double? Waist { get; set; }
        public double? Abdomen { get; set; }
        public double? Wrist { get; set; }
        public double? Hips { get; set; }
        public double? Thigh { get; set; }
        public double? Calf { get; set; }
        public double? Ankle { get; set; }
        public DateTime? Date { get; set; }

        public virtual CustomerModel CustomerModel { get; set; }
    }
}
