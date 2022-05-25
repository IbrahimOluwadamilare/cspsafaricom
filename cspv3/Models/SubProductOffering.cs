using System.ComponentModel.DataAnnotations;

namespace cspv3.Models
{
    public class SubProductOffering
    {
        [Key]
        public int id { get; set; }
        public string MeterCategory { get; set; }
        public string MeterSubCategory { get; set; }
        public string Region { get; set; }
        public string Name { get; set; }
        public string ResouceId { get; set; }
        public decimal WragbyPrice { get; set; }
        public int MinValue { get; set; }
        public string Units { get; set; }
    }
}
