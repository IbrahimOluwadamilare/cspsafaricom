using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels.SingleSku
{
    public partial class SingleSkuResponse
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }
        public bool IsTrial { get; set; }
        public List<string> SupportedBillingCycles { get; set; }
        public List<string> PurchasePrerequisites { get; set; }
        public List<string> InventoryVariables { get; set; }
        public List<string> ProvisioningVariables { get; set; }
        public Dictionary<string, string> DynamicAttributes { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string ObjectType { get; set; }
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
