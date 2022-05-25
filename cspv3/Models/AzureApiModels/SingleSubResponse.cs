using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels
{
    public partial class SingleSubResponse
    {
        public string Id { get; set; }
        public string EntitlementId { get; set; }
        public string OfferId { get; set; }
        public string OfferName { get; set; }
        public string FriendlyName { get; set; }
        public long Quantity { get; set; }
        public string UnitType { get; set; }
        public object ParentSubscriptionId { get; set; }
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
        public Links Links { get; set; }
        public string OrderId { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string Etag { get; set; }
        public string ObjectType { get; set; }
    }

    public partial class Links
    {
        public Offer Offer { get; set; }
        public Offer Self { get; set; }
    }

    public partial class Offer
    {
        [JsonProperty("Uri")]
        public string Url { get; set; }

        [JsonProperty("Method")]
        public string Methods { get; set; }

        [JsonProperty("Headers")]
        public List<object> Header { get; set; }
    }

}
