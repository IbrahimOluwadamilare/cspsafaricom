using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels
{
    public class CartLineItems
    {
        [JsonProperty("CatalogItemId")]
        public string CatalogItemId { get; set; }

        [JsonProperty("FriendlyName")]
        public string FriendlyName { get; set; }

        [JsonProperty("Quantity")]
        public long Quantity { get; set; }

        [JsonProperty("CurrencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("BillingCycle")]
        public string BillingCycle { get; set; }

        [JsonProperty("ProvisioningContext")]
        public ProvisioningContext ProvisioningContext { get; set; }
    }

    public partial class ProvisioningContext
    {
        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }
    }
}

