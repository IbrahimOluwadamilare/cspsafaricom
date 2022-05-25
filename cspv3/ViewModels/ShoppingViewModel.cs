using System;
using System.Collections.Generic;
using cspv3.Models;

namespace cspv3.ViewModels
{
    public class ShoppingViewModel
    {
        public ShoppingCartViewModel ShoppingCart { get; set; }
        public List<ProductOffering> Products { get; set; }
        public List<int> CartCount { get; set; }

    }
}
