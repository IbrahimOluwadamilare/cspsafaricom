using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels.SpResponse
{
    public partial class SingleProductResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProductTypeClass ProductType { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string ObjectType { get; set; }
    }

    public partial class Links
    {
        public Self Skus { get; set; }
        public Self Self { get; set; }
    }

    public partial class Self
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public List<object> Headers { get; set; }
    }

    public partial class ProductTypeClass
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public ProductTypeClass SubType { get; set; }
        public Attributes Attributes { get; set; }
    }

}
