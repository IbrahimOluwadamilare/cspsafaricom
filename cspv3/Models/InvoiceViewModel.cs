using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models
{
    public class InvoiceViewModels
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<ProductOffering> Products { get; set; }
        public IEnumerable<OrderDetail> Details { get; set; }


    }
}
