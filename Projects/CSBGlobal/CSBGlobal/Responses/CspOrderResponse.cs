using System;
using System.Collections.Generic;

namespace CSBGlobal.Services.OrderResponse
{
    public partial class CspOrderResponse
    {

        public string Id { get; set; }
        public string ReferenceCustomerId { get; set; }
        public string BillingCycle { get; set; }
        public string CurrencyCode { get; set; }
        public List<LineItem> LineItems { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string Status { get; set; }
        public CspOrderResponseLinks Links { get; set; }
        public CspOrderResponseAttributes Attributes { get; set; }
    }

    public partial class CspOrderResponseAttributes
    {
        public string Etag { get; set; }
        public string ObjectType { get; set; }
    }

    public partial class LineItem
    {
        public long LineItemNumber { get; set; }
        public string OfferId { get; set; }
        public string SubscriptionId { get; set; }
        public string ParentSubscriptionId { get; set; }
        public string FriendlyName { get; set; }
        public int Quantity { get; set; }
        public string PartnerIdOnRecord { get; set; }
        public LineItemLinks Links { get; set; }
        public LineItemAttributes Attributes { get; set; }
    }

    public partial class LineItemAttributes
    {
        public string ObjectType { get; set; }
    }

    public partial class LineItemLinks
    {
        public Self Subscription { get; set; }
        public Self Sku { get; set; }
        public Self ProvisioningStatus { get; set; }
    }

    public partial class Self
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public List<object> Headers { get; set; }
    }

    public partial class CspOrderResponseLinks
    {
        public Self Self { get; set; }
    }
}