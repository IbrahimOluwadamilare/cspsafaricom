using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Models.CspApiModels.Target
{

    public partial class TargetOfferModel
    {
        [JsonProperty("TargetOffer")]
        public TargetOffer TargetOffer { get; set; }

        [JsonProperty("UpgradeType")]
        public string UpgradeType { get; set; }

        [JsonProperty("IsEligible")]
        public bool IsEligible { get; set; }

        [JsonProperty("Quantity")]
        public long Quantity { get; set; }

        [JsonProperty("UpgradeErrors")]
        public List<UpgradeError> UpgradeErrors { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("Etag")]
        public string Etag { get; set; }

        [JsonProperty("ObjectType")]
        public string ObjectType { get; set; }
    }

    public partial class TargetOffer
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("MinimumQuantity")]
        public long MinimumQuantity { get; set; }

        [JsonProperty("MaximumQuantity")]
        public long MaximumQuantity { get; set; }

        [JsonProperty("Rank")]
        public long Rank { get; set; }

        [JsonProperty("Uri")]
        public string Uri { get; set; }

        [JsonProperty("Locale")]
        public string Locale { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Category")]
        public Category Category { get; set; }

        [JsonProperty("PrerequisiteOffers")]
        public List<string> PrerequisiteOffers { get; set; }

        [JsonProperty("IsAddOn")]
        public bool IsAddOn { get; set; }

        [JsonProperty("IsAvailableForPurchase")]
        public bool IsAvailableForPurchase { get; set; }

        [JsonProperty("Billing")]
        public string Billing { get; set; }

        [JsonProperty("SupportedBillingCycles")]
        public List<string> SupportedBillingCycles { get; set; }

        [JsonProperty("IsAutoRenewable")]
        public bool IsAutoRenewable { get; set; }

        [JsonProperty("UpgradeTargetOffers")]
        public List<string> UpgradeTargetOffers { get; set; }

        [JsonProperty("SalesGroupId")]
        public string SalesGroupId { get; set; }

        [JsonProperty("LimitUnitOfMeasure")]
        public string LimitUnitOfMeasure { get; set; }

        [JsonProperty("Limit")]
        public long Limit { get; set; }

        [JsonProperty("HasAddOns")]
        public bool HasAddOns { get; set; }

        [JsonProperty("ReselleeQualifications")]
        public List<string> ReselleeQualifications { get; set; }

        [JsonProperty("ResellerQualifications")]
        public List<string> ResellerQualifications { get; set; }

        [JsonProperty("IsTrial")]
        public bool IsTrial { get; set; }

        [JsonProperty("Product")]
        public Product Product { get; set; }

        [JsonProperty("UnitType")]
        public string UnitType { get; set; }

        [JsonProperty("Links")]
        public Links Links { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Rank")]
        public long Rank { get; set; }

        [JsonProperty("Locale")]
        public string Locale { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("LearnMore")]
        public LearnMore LearnMore { get; set; }

        [JsonProperty("Self")]
        public LearnMore Self { get; set; }
    }

    public partial class LearnMore
    {
    }

    public partial class Product
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Unit")]
        public string Unit { get; set; }
    }

    public partial class UpgradeError
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("AdditionalDetails")]
        public string AdditionalDetails { get; set; }
    }
}

