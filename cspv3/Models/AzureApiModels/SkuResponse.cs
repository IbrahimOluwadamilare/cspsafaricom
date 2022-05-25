using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels.SkuResponse
{
    public partial class SkuResponse
    {
     
    public long TotalCount { get; set; }
        public List<Item> Items { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string ObjectType { get; set; }
    }

    public partial class Item
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long MinimumQuantity { get; set; }
        public long MaximumQuantity { get; set; }
        public bool IsTrial { get; set; }
        public List<string> SupportedBillingCycles { get; set; }
        public object PurchasePrerequisites { get; set; }
        public object InventoryVariables { get; set; }
        public object ProvisioningVariables { get; set; }
        public DynamicAttributes DynamicAttributes { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class DynamicAttributes
    {
        public string BillingType { get; set; }
        public string Category { get; set; }
        public bool IsAddon { get; set; }
        public List<object> PrerequisiteSkus { get; set; }
    }

    public partial class Links
    {
        public Self Self { get; set; }
    }

    public partial class Self
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public List<object> Headers { get; set; }
    }
}