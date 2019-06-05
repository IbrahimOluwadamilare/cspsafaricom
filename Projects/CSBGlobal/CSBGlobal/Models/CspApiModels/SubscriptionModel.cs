using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Models.CspApiModels
{
    public partial class SubscriptionModel
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("EntitlementId")]
        public string EntitlementId { get; set; }

        [JsonProperty("OfferId")]
        public string OfferId { get; set; }

        [JsonProperty("OfferName")]
        public string OfferName { get; set; }

        [JsonProperty("FriendlyName")]
        public string FriendlyName { get; set; }

        [JsonProperty("Quantity")]
        public long Quantity { get; set; }

        [JsonProperty("UnitType")]
        public string UnitType { get; set; }

        [JsonProperty("ParentSubscriptionId")]
        public string ParentSubscriptionId { get; set; }

        [JsonProperty("CreationDate")]
        public DateTimeOffset CreationDate { get; set; }

        [JsonProperty("EffectiveStartDate")]
        public DateTimeOffset EffectiveStartDate { get; set; }

        [JsonProperty("CommitmentEndDate")]
        public DateTimeOffset CommitmentEndDate { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("AutoRenewEnabled")]
        public bool AutoRenewEnabled { get; set; }

        [JsonProperty("BillingType")]
        public string BillingType { get; set; }

        [JsonProperty("BillingCycle")]
        public string BillingCycle { get; set; }

        [JsonProperty("PartnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("SuspensionReasons")]
        public List<string> SuspensionReasons { get; set; }

        [JsonProperty("Links")]
        public Links Links { get; set; }

        [JsonProperty("OrderId")]
        public string OrderId { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("Etag")]
        public string tag { get; set; }

        [JsonProperty("ObjectType")]
        public string objType { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("Offer")]
        public Offer Offer { get; set; }

        [JsonProperty("ParentSubscription")]
        public Offer ParentSubscription { get; set; }

        [JsonProperty("Self")]
        public Offer sel { get; set; }
    }

    public partial class Offer
    {
    }
}



