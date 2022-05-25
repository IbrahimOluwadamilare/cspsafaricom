using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels.Availability
{
    public partial class ProductAvailabilityResponse
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
        public string SkuId { get; set; }
        public string CatalogItemId { get; set; }
        public DefaultCurrency DefaultCurrency { get; set; }
        public string Segment { get; set; }
        public string Country { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class DefaultCurrency
    {
        public string Code { get; set; }
        public string Symbol { get; set; }
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
