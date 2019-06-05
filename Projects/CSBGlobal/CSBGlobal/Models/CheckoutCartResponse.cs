using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Models.Checkout
{
    public class CheckoutCartResponse
    {
        [JsonProperty("Orders")]
        public List<Order> Orders { get; set; }

        [JsonProperty("OrderErrors")]
        public List<object> OrderErrors { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("ObjectType")]
        public string ObjectType { get; set; }
    }

    public partial class Order
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("ReferenceCustomerId")]
        public Guid ReferenceCustomerId { get; set; }

        [JsonProperty("BillingCycle")]
        public string BillingCycle { get; set; }

        [JsonProperty("CurrencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("LineItems")]
        public List<LineItem> LineItems { get; set; }

        [JsonProperty("CreationDate")]
        public DateTimeOffset CreationDate { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Links")]
        public OrderLinks Links { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }
    }

    public partial class LineItem
    {
        [JsonProperty("LineItemNumber")]
        public long LineItemNumber { get; set; }

        [JsonProperty("OfferId")]
        public string OfferId { get; set; }

        [JsonProperty("SubscriptionId")]
        public object SubscriptionId { get; set; }

        [JsonProperty("ParentSubscriptionId")]
        public object ParentSubscriptionId { get; set; }

        [JsonProperty("FriendlyName")]
        public string FriendlyName { get; set; }

        [JsonProperty("Quantity")]
        public long Quantity { get; set; }

        [JsonProperty("PartnerIdOnRecord")]
        public object PartnerIdOnRecord { get; set; }

        [JsonProperty("Links")]
        public LineItemLinks Links { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }
    }

    public partial class LineItemLinks
    {
        [JsonProperty("Sku")]
        public ProvisioningStatus Sku { get; set; }
    }

    public partial class ProvisioningStatus
    {
        [JsonProperty("Uri")]
        public string Uri { get; set; }

        [JsonProperty("Method")]
        public string Method { get; set; }

        [JsonProperty("Headers")]
        public List<object> Headers { get; set; }
    }

    public partial class OrderLinks
    {
        [JsonProperty("ProvisioningStatus")]
        public ProvisioningStatus ProvisioningStatus { get; set; }

        [JsonProperty("Self")]
        public ProvisioningStatus Self { get; set; }
    }
}
