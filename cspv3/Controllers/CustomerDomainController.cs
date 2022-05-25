using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using cspv3.Data;
using cspv3.Models;
using cspv3.Services;
using cspv3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cspv3.Controllers
{
    [Authorize]
    public class CustomerDomainController : Controller
    {
        private IDomainService _domainservice;
        private readonly IShoppingCart _shoppingCart;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public CustomerDomainController(IShoppingCart shoppingCart, ApplicationDbContext Context, IDomainService domainService, UserManager<ApplicationUser> userManager)
        {
            _domainservice = domainService;
            _shoppingCart = shoppingCart;
            _userManager = userManager;

            _dbContext = Context;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BuyDomain()
        {
            return View();
        }
        public async Task<IActionResult> UseExternalDomain()
        {
           

            return View();
        }

        public async Task<IActionResult> CheckDomainAvailability(string domain)
        {
            var avail = await _domainservice.GetDomainAvailabilityAsync(domain);

            return Json(avail);
        }

        [ValidateAntiForgeryToken]

        [HttpPost]

        public async Task<IActionResult> DomainOrder(string domainName, string domainPrice)
        {
            var cart = _shoppingCart.GetCart(this.HttpContext);
            ShoppingCartViewModel model = new ShoppingCartViewModel();

            List<ProductOffering> products = new List<ProductOffering>();
            var items = await cart.GetCartItemsAsync();


            model.CartItems = items.ToList();

            var domaincost = decimal.Parse(domainPrice) / 1000000 * 370;

            model.CartTotal = await cart.GetTotal() + domaincost;


            var user = await _userManager.GetUserAsync(User);
            model.User = user;

            foreach (var item in model.CartItems)
            {

                products.Add(_dbContext.Products.FirstOrDefault(Opt => Opt.cspID.ToLower() == item.ProductId.ToLower()));

            }
            model.Products = products;

            model.DomainName = domainName;
            model.DomainCost = domaincost;

            var dom = _dbContext.Domains.Where(d => d.OwnerId == user.Id && d.OrderId == 0);

            if (dom != null & dom.Count() > 0)
            {
                
                
                _dbContext.Domains.RemoveRange(dom);
              

            }
           
                var domain = new DomainOffer
                {
                    DomainName = domainName,
                    OwnerId = user.Id,

                };
                _dbContext.Add(domain);
                _dbContext.SaveChanges();
            

            
           
            

            return View(model);
        }
    }
    }