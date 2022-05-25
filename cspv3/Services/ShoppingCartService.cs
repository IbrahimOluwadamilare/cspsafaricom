using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cspv3.Data;
using cspv3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace cspv3.Services
{
    public class ShoppingCartService : IShoppingCart
    {

        private readonly ApplicationDbContext _dbContext;
        IServiceProvider _services;
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public ShoppingCartService(ApplicationDbContext Context, IServiceProvider services)
        {
            _dbContext = Context;
            _services = services;
        }

        public ShoppingCartService GetCart(HttpContext context)
        {
            var cart = new ShoppingCartService(_dbContext, _services);
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        public string GetCartId(HttpContext context)
        {


            if (context.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session.SetString(CartSessionKey, context.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            var id = context.Session.GetString(CartSessionKey);
            return id;
        }
        public async Task AddToCart(ProductOffering product)
        {
            var shoppingCartItem =
                _dbContext.Carts.SingleOrDefault(
                    s => s.CartId == ShoppingCartId && s.ProductId == product.cspID);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new Cart
                {

                    ProductId = product.cspID,
                    CartId = ShoppingCartId,
                    Count = 1,
                    Product = product,
                    DateCreated = DateTime.Now
                };

                _dbContext.Carts.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Count++;
            }
            _dbContext.SaveChanges();
        }

        public async Task AddVmToCart(SubProductOffering product)
        {
            var shoppingCartItem =
                _dbContext.VmCarts.SingleOrDefault(
                    s => s.CartId == ShoppingCartId && s.ProductId == product.ResouceId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new VmCart
                {

                    ProductId = product.ResouceId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    Productlink = product,
                    DateCreated = DateTime.Now
                };

                _dbContext.VmCarts.Add(shoppingCartItem);
            }
            else
            {
                // shoppingCartItem.Count++;
            }
            _dbContext.SaveChanges();
        }

        public async Task<int> CreateVmOrderAsync(Order order)
        {

            var cartItems = await GetVmCartItemsAsync();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {

                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = _dbContext.SubProducts.FirstOrDefault(opt => opt.ResouceId == item.ProductId).WragbyPrice,
                    Quantity = item.Count

                };

                _dbContext.OrderDetails.Add(orderDetail);

            }

            // Save the order
            _dbContext.SaveChanges();
            // Empty the shopping cart
            await EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }
        public async Task<int> CreateOrderAsync(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = await GetCartItemsAsync();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {

                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    Product = item.Product,
                    OrderId = order.OrderId,
                    UnitPrice = _dbContext.Products.FirstOrDefault(opt => opt.cspID == item.ProductId).WragbyPrice,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * orderDetail.UnitPrice);

                _dbContext.OrderDetails.Add(orderDetail);

            }
            var result = (0.05 * Convert.ToInt32(orderTotal));
            // Set the order's total to the orderTotal count
            order.Total = orderTotal + Convert.ToDecimal(result);

            // Save the order
            _dbContext.SaveChanges();
            // Empty the shopping cart
            await EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        public async Task EmptyCart()
        {
            var cartItems = await _dbContext
                .Carts
                .Where(cart => cart.CartId == ShoppingCartId).ToListAsync();

            foreach (var cartItem in cartItems)
            {
                _dbContext.Carts.Remove(cartItem);
            }

            _dbContext.SaveChanges();

        }



        public async Task<IEnumerable<Cart>> GetCartItemsAsync()
        {
            return await _dbContext.Carts.Include(a => a.Product).Where(
                cart => cart.CartId == ShoppingCartId).ToListAsync();
        }
        public async Task<IEnumerable<VmCart>> GetVmCartItemsAsync()
        {
            return await _dbContext.VmCarts.Where(
                cart => cart.CartId == ShoppingCartId).ToListAsync();
        }

        public async Task<int> GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _dbContext.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public async Task<decimal> GetTotal()
        {
            decimal total = _dbContext.Carts.Where(c => c.CartId == ShoppingCartId).Select(
                c => c.Count * c.Product.WragbyPrice).Sum();

            return total;
        }

        public async Task MigrateCart(string userName)
        {
            var shoppingCart = await _dbContext.Carts.Where(
                c => c.CartId == ShoppingCartId).ToListAsync();

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _dbContext.SaveChanges();
        }

        public async Task<int> RemoveFromCart(string id)
        {

            var shoppingCartItem =
                _dbContext.Carts.SingleOrDefault(
                    s => s.CartId == ShoppingCartId && s.ProductId == id);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {

                _dbContext.Carts.Remove(shoppingCartItem);

            }

            _dbContext.SaveChanges();

            return localAmount;
        }




        public async Task<int> IncrementCart(string id)
        {

            var shoppingCartItem =
                _dbContext.Carts.SingleOrDefault(
                    s => s.CartId == ShoppingCartId && s.ProductId == id);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {

                shoppingCartItem.Count++;
                localAmount = shoppingCartItem.Count;


            }

            _dbContext.SaveChanges();

            return localAmount;
        }

        public async Task<int> UpdateCart(string id, int count )
        {

            var shoppingCartItem =
                _dbContext.Carts.SingleOrDefault(
                    s => s.CartId == ShoppingCartId && s.ProductId == id);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {

                shoppingCartItem.Count = count;
                localAmount = shoppingCartItem.Count;


            }

           await _dbContext.SaveChangesAsync();

            return localAmount;
        }

        public async Task<int> DecrementCart(string id)
        {

            var shoppingCartItem =
                _dbContext.Carts.SingleOrDefault(
                    s => s.CartId == ShoppingCartId && s.ProductId == id);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Count > 1)
                {
                    shoppingCartItem.Count--;
                    localAmount = shoppingCartItem.Count;
                }
                else
                {
                    _dbContext.Carts.Remove(shoppingCartItem);
                }
            }

            _dbContext.SaveChanges();

            return localAmount;
        }



        public async Task<int> UpdateCartCount(int id, int cartCount)
        {
            // Get the cart 
            var cartItem = _dbContext.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartCount > 0)
                {
                    cartItem.Count = cartCount;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _dbContext.Carts.Remove(cartItem);
                }
                // Save changes 
                _dbContext.SaveChanges();
            }
            return itemCount;
        }
    }
}
