using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cspv3.ViewModels;

using cspv3.Services;
using cspv3.Data;
using cspv3.Models;
using Microsoft.AspNetCore.Http;
 
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace cspv3.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IShoppingCart _shoppingCart;
        public ShoppingCartController(IShoppingCart shoppingCart, ApplicationDbContext Context, UserManager<ApplicationUser> userManager)
        {
            _shoppingCart = shoppingCart;
            _dbContext = Context;
            _userManager = userManager;




        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {

            var cart = _shoppingCart.GetCart(this.HttpContext);
            ShoppingCartViewModel model = new ShoppingCartViewModel();

            List<ProductOffering> products = new List<ProductOffering>();
            var items = await cart.GetCartItemsAsync();


            model.CartItems = items.ToList();
            model.CartTotal = await cart.GetTotal();


            foreach (var item in model.CartItems)
            {

                products.Add(_dbContext.Products.FirstOrDefault(Opt => Opt.cspID.ToLower() == item.ProductId.ToLower()));

            }
            model.Products = products;
            return View(model);

        }


     


        public async Task<IActionResult> ClearCart()
        {
            var cart = _shoppingCart.GetCart(this.HttpContext);
            await cart.EmptyCart();
            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            return View();
        }

        public async Task<IActionResult> AddToCart(string id)
        {
            var cart = _shoppingCart.GetCart(this.HttpContext);

            var addedProducts = _dbContext.Products
                .FirstOrDefault(product => product.cspID == id);
            if (addedProducts != null)
            {
                await cart.AddToCart(addedProducts);
            }
            
             return RedirectToAction("Index");
        }

      

        public async Task<IActionResult> RemoveItem(string id)
        {
            var cart = _shoppingCart.GetCart(this.HttpContext);
            var count = await cart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> GenerateInvoice()
        {
            var cart = _shoppingCart.GetCart(this.HttpContext);
            ShoppingCartViewModel model = new ShoppingCartViewModel();

            List<ProductOffering> products = new List<ProductOffering>();
            var items = await cart.GetCartItemsAsync();


            model.CartItems = items.ToList();
            model.CartTotal = await cart.GetTotal();


            model.User = await _userManager.GetUserAsync(User);

            // model.User = await _userManager.FindByEmailAsync(HttpContext.Session.GetString("Email"));

            foreach (var item in model.CartItems)
            {

                products.Add(_dbContext.Products.FirstOrDefault(Opt => Opt.cspID.ToLower() == item.ProductId.ToLower()));

            }
            model.Products = products;

            //return new ViewAsPdf(nameof(GenerateInvoice), model);
            return View(model);

        }
        public async Task<IActionResult> Invoice()
        {
            var cart = _shoppingCart.GetCart(this.HttpContext);
            ShoppingCartViewModel model = new ShoppingCartViewModel();

            List<ProductOffering> products = new List<ProductOffering>();
            var items = await cart.GetCartItemsAsync();


            model.CartItems = items.ToList();
            model.CartTotal = await cart.GetTotal();


            model.User = await _userManager.GetUserAsync(User);

            // model.User = await _userManager.FindByEmailAsync(HttpContext.Session.GetString("Email"));

            foreach (var item in model.CartItems)
            {

                products.Add(_dbContext.Products.FirstOrDefault(Opt => Opt.cspID.ToLower() == item.ProductId.ToLower()));

            }
            model.Products = products;

            //return new ViewAsPdf(nameof(GenerateInvoice), model);
            return View(model);

        }
        public async Task<IActionResult> Increment(string id)
        {
            bool success = false;
            // save product
            success = true;
            
            var cart = _shoppingCart.GetCart(this.HttpContext);
            var count = await cart.IncrementCart(id);
            //return new JsonResult(success);
           return RedirectToAction("Index");
        }
        public async Task<IActionResult> Decrement(string id)
        {
            var cart = _shoppingCart.GetCart(this.HttpContext);
            var count = await cart.DecrementCart(id);
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Annualbilling(string id)
        //{
        //    var cart = _shoppingCart.GetCart(this.HttpContext);
        //    var shoppingCart = await cart.GetCartItemsAsync();
        //    var ca = shoppingCart.FirstOrDefault(a => a.CartId == id);



        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult> UpdateCart(int value, string id)
        {
            try
            {
                if (value == 0)
                {
                    return Json(new { isSuccess = false, message = "wrong value"});

                }
                var cart = _shoppingCart.GetCart(this.HttpContext);
                var count = await cart.UpdateCart(id, value);
                return Json(new { isSuccess = true, message = "", redirecturl = Url.Action(nameof(Index))  });

            }
            catch (Exception)
            {

                return Json(new { isSuccess = false });
            }


        }




    }




}





