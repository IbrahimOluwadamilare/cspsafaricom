using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cspv3.Models;
using Microsoft.AspNetCore.Authorization;
using cspv3.Data;
using cspv3.Services;
using NToastNotify;

namespace cspv3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private IProductOffering _productOffering;
        private readonly IAzureApi _azureservice;
        private readonly ICSPapi _cspApi;
        private readonly IToastNotification _toastNotification;

        public HomeController(IProductOffering productOffering, IAzureApi azureApi, ICSPapi CspApi, ApplicationDbContext context, IToastNotification toastNotification)
        {
            _productOffering = productOffering;
            _context = context;
            _toastNotification = toastNotification;

        }

        //[Authorize]
        public IActionResult Index()
        {
            var products = _context.Products.Where(p => p.category == "Wragby Bundle").ToList();


            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult BusinessEssentialsInfo(int id)
        {

            var product = _context.Products.FirstOrDefault(a => a.Id == id);

            //List<ProductOffering> productOffering = _context.Products.ToList();
            return View(product);
        }
        public IActionResult BusinessPremiumInfo()
        {
            return View();
        }
        public IActionResult BusinessInfo()
        {
            return View();
        }
        public IActionResult ShoppingCart()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }
        public IActionResult BundlesPage()
        {
            var products = _context.Products.Where(p => p.category == "Wragby Bundle").ToList(); return View(products);

        }
        public IActionResult CheckOutPage()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
        public IActionResult TermsandConditions()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Contact(ContactSupport model)
        {
            if (ModelState.IsValid)
            {
                model.DateCreated = DateTime.Now;
                var ticket = _context.Add(model);
                _context.SaveChanges();

                ViewBag.Status = "Help Request Sent Successfully, You will be Contacted Shortly";

                _toastNotification.AddSuccessToastMessage("Help Request Sent Successfully, You will be Contacted Shortly", new NotyOptions() { Theme = "metroui", ProgressBar = false, Timeout = 10000 });


            }
          
         _toastNotification.AddErrorToastMessage("Unable to submit help request", new NotyOptions() { Theme = "metroui", ProgressBar = false, Timeout = 10000 });

            ModelState.Clear();
            return View();


        }
        public IActionResult LicencedBasedProducts(int? page, string productCategory)
        {
            IQueryable<ProductOffering> productlistoffering = _productOffering.ProductOfferings(productCategory);
            

            ViewBag.ProductCategory = productCategory;
            return View(productlistoffering.ToList());
           
        }
        [Authorize]
        public async Task<IActionResult> ProductDetails(string Id)
        {
            var products = await _azureservice.GetSKUs(Id);

            //return View(_productOffering.SubProductOfferings(productCategory));
            return View(products);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
