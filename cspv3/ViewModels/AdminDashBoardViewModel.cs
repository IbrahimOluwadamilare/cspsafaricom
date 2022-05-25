using cspv3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.ViewModels
{
    public class AdminDashBoardViewModel
    {
        public IEnumerable<Order> Order { get; set; }
        public IEnumerable<ProductOffering> Product { get; set; }

    }
}
