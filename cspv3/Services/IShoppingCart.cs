using System;
using System.Threading.Tasks;
using cspv3.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
namespace cspv3.Services
{
    public interface IShoppingCart
    {
        ShoppingCartService GetCart(HttpContext context);

        Task AddToCart(ProductOffering product);

        Task<int> RemoveFromCart(string id);

        Task EmptyCart();

        Task<IEnumerable<Cart>> GetCartItemsAsync();

        Task<int> GetCount();

        Task<decimal> GetTotal();

        Task<int> CreateOrderAsync(Order order);

        Task MigrateCart(string userName);

        Task<int> UpdateCartCount(int id, int cartCount);
    }
}
