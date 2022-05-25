using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.CspApiModels.Order
{
    public class OrderModel
    {

        public string ReferenceCustomerId { get; set; }
        public string BillingCycle { get; set; }
        public List<LineItem> LineItems { get; set; }
    }

    public partial class LineItem
    {
        public int LineItemNumber { get; set; }
        public string OfferId { get; set; }
        public string FriendlyName { get; set; }
        public int Quantity { get; set; }
    }
}