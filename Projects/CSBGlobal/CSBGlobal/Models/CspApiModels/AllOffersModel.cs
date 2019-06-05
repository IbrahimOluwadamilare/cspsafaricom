

namespace CSBGlobal.Models.CspApiModels.Offers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class AllOffersModel
    {
        public int TotalCount { get; set; }
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
        public int Rank { get; set; }
        public string Uri { get; set; }
        public string Locale { get; set; }
        public string Country { get; set; }
        public Category Category { get; set; }
        public List<string> PrerequisiteOffers { get; set; }
        public bool IsAddOn { get; set; }
        public bool IsAvailableForPurchase { get; set; }
        public string Billing { get; set; }
        public List<string> SupportedBillingCycles { get; set; }
        public bool IsAutoRenewable { get; set; }
        public object UpgradeTargetOffers { get; set; }
        public string SalesGroupId { get; set; }
        public string LimitUnitOfMeasure { get; set; }
        public int Limit { get; set; }
        public bool HasAddOns { get; set; }
        public List<object> ReselleeQualifications { get; set; }
        public List<object> ResellerQualifications { get; set; }
        public bool IsTrial { get; set; }
        public Product Product { get; set; }
        public string UnitType { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string ObjectType { get; set; }
    }

    public partial class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public string Locale { get; set; }
        public string Country { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Links
    {
        public LearnMore LearnMore { get; set; }
        public LearnMore Self { get; set; }
    }

    public partial class LearnMore
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public List<object> Headers { get; set; }
    }

    public partial class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }

    // public string category { get; set; }
}

