using cspv3.Models;
using System.Collections.Generic;

namespace cspv3.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<ProductOffering> Products { get; set; }
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public ApplicationUser User { get; set; }


        public string DomainName { get; set; }
        public decimal DomainCost { get; set; }
    }
}