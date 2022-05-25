using cspv3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.ViewModels
{
    public class OrderDetailsViewModel
    {
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<ProductOffering> Products { get; set; }
    }
}
