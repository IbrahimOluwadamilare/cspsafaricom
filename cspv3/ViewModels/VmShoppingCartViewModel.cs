using cspv3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.ViewModels
{
    public class VmShoppingCartViewModel
    {
        public string CartItems { get; set; }
        public ApplicationUser User { get; set; }
       public string Productid { get; set; }
      public  string Skuid { get; set; }
    }
}
