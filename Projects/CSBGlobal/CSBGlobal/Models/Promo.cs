using System.ComponentModel.DataAnnotations;

namespace CSBGlobal.Models
{
    public class Promo
    {
        [Key]
        public int Id { get; set; }
        public string PromoCode { get; set; }
        public decimal PercentagePayment { get; set; }
        public decimal PercentageInvoice { get; set; }
        public string Authorization { get; set; }
        public System.DateTime ExpirationDate { get; set; }
    }
}
