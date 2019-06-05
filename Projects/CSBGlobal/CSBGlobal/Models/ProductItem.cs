using System.Collections.Generic;
namespace CSBGlobal.Models
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
        public int Rank { get; set; }
        public string Uri { get; set; }
        public string Locale { get; set; }
        public string Country { get; set; }
        public string CspId { get; set; }
        public List<string> PrerequisiteOffers { get; set; }
        public string IsAddOn { get; set; }
        public string Billing { get; set; }
        public List<string> SupportedBillingCycles { get; set; }
        public List<string> UpgradeTargetOffers { get; set; }
        public string SalesGroupId { get; set; }
        public string LicenseAgreementType { get; set; }
        public string SecondaryLicenseType { get; set; }
        public string EndCustomerType { get; set; }
        public decimal WragbyPrice { get; set; }
    }
}
