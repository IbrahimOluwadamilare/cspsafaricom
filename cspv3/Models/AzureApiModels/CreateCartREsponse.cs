using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels.CartResponse
{
    public class CreateCartResponse
    {
        public string Id { get; set; }
        public DateTimeOffset CreationTimestamp { get; set; }
        public DateTimeOffset LastModifiedTimestamp { get; set; }
        public DateTimeOffset ExpirationTimestamp { get; set; }
        public Guid LastModifiedUser { get; set; }
        public LineItem[] LineItems { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string ObjectType { get; set; }
    }

    public partial class LineItem
    {
        public int Id { get; set; }
        public string CatalogItemId { get; set; }
        public string FriendlyName { get; set; }
        public int Quantity { get; set; }
        public string CurrencyCode { get; set; }
        public string BillingCycle { get; set; }
        public object Participants { get; set; }
        public ProvisioningContext ProvisioningContext { get; set; }
        public int OrderGroup { get; set; }
        public Error Error { get; set; }
    }
    public partial class Error
    {
        public string ErrorCode { get; set; }

        public string ErrorDescription { get; set; }
    }

    public partial class ProvisioningContext
    {
        public string SubscriptionId { get; set; }
        public string Scope { get; set; }
        public string Duration { get; set; }
    }

    public partial class Links
    {
        public Self Self { get; set; }
    }

    public partial class Self
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public object[] Headers { get; set; }
    }
}

