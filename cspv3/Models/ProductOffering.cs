using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cspv3.Models
{
    public class ProductOffering
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MinimumQuantity { get; set; }
        public int? MaximumQuantity { get; set; }
        public int? Rank { get; set; }
        public string Uri { get; set; }
        public string Locale { get; set; }
        public string Country { get; set; }
        public string cspID { get; set; }
        public List<PrerequisiteOffer> PrerequisiteOffers { get; set; }
        public string IsAddOn { get; set; }
        public string Billing { get; set; }
        public List<SupportedBillingCycle> SupportedBillingCycles { get; set; }
        public List<UpgradeTargetOffer> UpgradeTargetOffers { get; set; }
        public string SalesGroupId { get; set; }
        public string LicenseAgreementType { get; set; }
        public string SecondaryLicenseType { get; set; }
        public string EndCustomerType { get; set; }
        public decimal WragbyPrice { get; set; }
        public string category { get; set; }
    }
    public class PrerequisiteOffer
    {
        [Key]
        public int Id { get; set; }
        public string PrereqOffer { get; set; }
    }
    public class SupportedBillingCycle
    {
        [Key]
        public int Id { get; set; }
        public string SupBillingCycle { get; set; }
    }
    public class UpgradeTargetOffer
    {
        [Key]
        public int Id { get; set; }
        public string UpgTargetOffer { get; set; }
    }
}