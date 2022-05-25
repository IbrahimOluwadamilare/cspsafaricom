namespace cspv3.ViewModels
{
    public class PromotionModel
    {
        public string PromoCode { get; set; }
        public decimal PercentagePayment { get; set; }
        public decimal PercentageInvoice { get; set; }
        public string authorization { get; set; }
        public System.DateTime ExpirationDate { get; set; }
    }
}