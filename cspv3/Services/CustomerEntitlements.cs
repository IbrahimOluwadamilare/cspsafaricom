using Newtonsoft.Json;
using System.Collections.Generic;

namespace cspv3.Services
{
    public class CustomerEntitlements
    {

        public long TotalCount { get; set; }
        public List<object> Items { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("ObjectType")]

        public string Type { get; set; }
    }

    public partial class Links
    {
    }

}
