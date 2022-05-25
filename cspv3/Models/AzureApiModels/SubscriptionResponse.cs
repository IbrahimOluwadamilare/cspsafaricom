using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels.ASubResponse
{
    public partial class SubcriptionResponse
    {
        public int TotalCount { get; set; }
        public List<Item> Items { get; set; }
        public SubcriptionResponseLinks Links { get; set; }
        public SubcriptionResponseAttributes Attributes { get; set; }
    }

    public partial class SubcriptionResponseAttributes
    {
        public string stringType { get; set; }
    }

    public partial class Item
    {
        public string Id { get; set; }
        public string EntitlementId { get; set; }
        public string OfferId { get; set; }
        public string OfferName { get; set; }
        public string FriendlyName { get; set; }
        public int Quantity { get; set; }
        public string UnitType { get; set; }
        public string ParentSubscriptionId { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset EffectiveStartDate { get; set; }
        public DateTimeOffset CommitmentEndDate { get; set; }
        public string Status { get; set; }
        public bool AutoRenewEnabled { get; set; }
        public string BillingType { get; set; }
        public string BillingCycle { get; set; }
        public object PartnerId { get; set; }
        public object SuspensionReasons { get; set; }
        public string ContractType { get; set; }
        public ItemLinks Links { get; set; }
        public string OrderId { get; set; }
        public ItemAttributes Attributes { get; set; }
    }

    public partial class ItemAttributes
    {
        public string Etag { get; set; }
        public string ObjectType { get; set; }
    }

    public partial class ItemLinks
    {
        public Offer Offer { get; set; }
        public Offer Self { get; set; }
    }

    public partial class Offer
    {

        public string Uri { get; set; }
        public string Method { get; set; }
        public List<object> Headers { get; set; }
    }

    public partial class SubcriptionResponseLinks
    {
    }

}
