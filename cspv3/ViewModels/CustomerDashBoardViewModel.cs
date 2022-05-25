using cspv3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.ViewModels
{
    public class CustomerDashBoardViewModel
    {
        public IEnumerable<Order> Order { get; set; }
        public IEnumerable<OrderDetail> orderDetails { get; set; }
        public ApplicationUser user { get; set; }

        public IEnumerable<Invoice> Invoice { get; set; }

    }
}
