using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Models
{
    public class InvoiceViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<ProductOffering> Products { get; set; }
        public IEnumerable<OrderDetail> Details { get; set; }


    }
}
