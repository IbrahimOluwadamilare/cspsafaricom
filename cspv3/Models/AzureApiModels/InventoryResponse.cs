using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels
{
    public partial class InventoryResponse
    {
        [JsonProperty("ProductId")]
        public string ProductId { get; set; }

        [JsonProperty("SkuId")]
        public string SkuId { get; set; }

        [JsonProperty("IsRestricted")]
        public bool IsRestricted { get; set; }

        [JsonProperty("Restrictions")]
        public object[] Restrictions { get; set; }
    }

}


