using System;
namespace cspv3.Models.DomainModels
{
    public partial class DomainPurchaseResponseModel
    {
        public int OrderId { get; set; }
        public int ItemCount { get; set; }
        public int Total { get; set; }
        public string Currency { get; set; }
    }

}
