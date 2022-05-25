using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.CspApiModels.Licenses
{
    public partial class LicensesResponse
    {
        public int TotalCount { get; set; }
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
        public List<ServicePlan> ServicePlans { get; set; }
        public ProductSku ProductSku { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class ProductSku
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SkuPartNumber { get; set; }
        public object TargetType { get; set; }
        public string LicenseGroupId { get; set; }
    }

    public partial class ServicePlan
    {
        public string DisplayName { get; set; }
        public string ServiceName { get; set; }
        public string Id { get; set; }
        public string CapabilityStatus { get; set; }
        public string TargetType { get; set; }
    }

    public partial class Links
    {
    }


}
