namespace cspv3.Models.Products
{
    public class ProductfromCSP
    {
        public int TotalCount { get; set; }
        public OfferItem[] Items { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public class Links
    {
        public Self Self { get; set; }
    }

    public class Self
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public object[] Headers { get; set; }
    }

    public class Attributes
    {
        public string ObjectType { get; set; }
    }
    //[JsonArray]
    public class OfferItem
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
        public string cspid { get; set; }
        public string[] PrerequisiteOffers { get; set; }
        public string IsAddOn { get; set; }
        public string Billing { get; set; }
        public string[] SupportedBillingCycles { get; set; }
        public string[] UpgradeTargetOffers { get; set; }
        public string SalesGroupId { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public string Locale { get; set; }
        public string Country { get; set; }
        public FirstAttributes Attributes { get; set; }
    }

    public class FirstAttributes
    {
        public string ObjectType { get; set; }
    }

    public class Product
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
        public string cspid { get; set; }
        public string[] PrerequisiteOffers { get; set; }
        public string IsAddOn { get; set; }
        public string Billing { get; set; }
        public string[] SupportedBillingCycles { get; set; }
        public string[] UpgradeTargetOffers { get; set; }
        public string SalesGroupId { get; set; }
        public string Unit { get; set; }
    }

    public class FirstLinks
    {
        public Learnmore LearnMore { get; set; }
        public FirstSelf Self { get; set; }
    }

    public class Learnmore
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public object[] Headers { get; set; }
    }

    public class FirstSelf
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public object[] Headers { get; set; }
    }

    public class SecondAttributes
    {
        public string ObjectType { get; set; }
    }

}