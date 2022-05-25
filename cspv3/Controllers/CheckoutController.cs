using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cspv3.Configuration;
using cspv3.Data;
using cspv3.Helpers;
using cspv3.Models;
using cspv3.Models.AzureApiModels;
using cspv3.Models.CspApiModels;
using cspv3.Models.DomainModels;
using cspv3.Models.DomainModelse;
using cspv3.Services;
using cspv3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PayStack.Net;
using Rotativa.AspNetCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cspv3.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private AppSettings AppSettings { get; set; }

        private IEmailSender _emailservice;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFirstCheckout _firstCheckout;
        private readonly IShoppingCart _shoppingCart;
        private readonly ICSPapi _cspApi;
        private readonly IAzureApi _azureservice;
        private IDomainService _domainservice;
        private IHttpContextAccessor _accessor;

        public CheckoutController(IFirstCheckout firstCheckout, IShoppingCart shoppingCart, ApplicationDbContext Context, IOptions<AppSettings> settings, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICSPapi CspApi, IEmailSender emailSender, IAzureApi azureApi, IDomainService domainService, IHttpContextAccessor accessor)
        {
            _firstCheckout = firstCheckout;
            _userManager = userManager;
            _signInManager = signInManager;
            _shoppingCart = shoppingCart;
            _cspApi = CspApi;
            _dbContext = Context;
            AppSettings = settings.Value;
            _azureservice = azureApi;
            _domainservice = domainService;
            _accessor = accessor;


            _emailservice = emailSender;
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


            model.User = await _userManager.GetUserAsync(User);

            // model.User = await _userManager.FindByEmailAsync(HttpContext.Session.GetString("Email"));

            foreach (var item in model.CartItems)
            {

                products.Add(_dbContext.Products.FirstOrDefault(Opt => Opt.cspID.ToLower() == item.ProductId.ToLower()));

            }
            model.Products = products;
            ViewBag.BillingCycle = "Monthly";

            return View(model);
        }
        public async Task<IActionResult> BiAnnualBilling()
        {


            var cart = _shoppingCart.GetCart(this.HttpContext);
            ShoppingCartViewModel model = new ShoppingCartViewModel();

            List<ProductOffering> products = new List<ProductOffering>();
            var items = await cart.GetCartItemsAsync();


            model.CartItems = items.ToList();
            model.CartTotal = (await cart.GetTotal() * 6);


            model.User = await _userManager.GetUserAsync(User);


            foreach (var item in model.CartItems)
            {

                products.Add(_dbContext.Products.FirstOrDefault(Opt => Opt.cspID.ToLower() == item.ProductId.ToLower()));

            }
            model.Products = products;
            ViewBag.BillingCycle = "BiAnnual";
            return View(nameof(Index), model);
        }
        public async Task<IActionResult> QuarterlyBilling()
        {


            var cart = _shoppingCart.GetCart(this.HttpContext);
            ShoppingCartViewModel model = new ShoppingCartViewModel();

            List<ProductOffering> products = new List<ProductOffering>();
            var items = await cart.GetCartItemsAsync();


            model.CartItems = items.ToList();
            model.CartTotal = (await cart.GetTotal() * 3);


            model.User = await _userManager.GetUserAsync(User);


            foreach (var item in model.CartItems)
            {

                products.Add(_dbContext.Products.FirstOrDefault(Opt => Opt.cspID.ToLower() == item.ProductId.ToLower()));

            }
            model.Products = products;
            ViewBag.BillingCycle = "Quarterly";
            return View(nameof(Index), model);
        }

        public IActionResult RenewSubscription(int id)
        {
            var orderitem = _dbContext.Orders.Include(i => i.OrderDetails).ThenInclude(b => b.Product).Where(c => c.Email == User.Identity.Name).SingleOrDefault(c => c.OrderId == id);

            if (orderitem == null)
            {
                return NotFound();
            }

            return View(orderitem);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RenewalPayment(int orderId, string Reference)
        {
            var orderitem = _dbContext.Orders.Where(c => c.Email == User.Identity.Name).SingleOrDefault(c => c.OrderId == orderId);

            orderitem.Refinfo = Reference;

            _dbContext.Update(orderitem);
            _dbContext.SaveChanges();

            if (orderitem == null)
            {
                return Json(new { isSuccess = false });
            }

            return Json(new { isSuccess = true });


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaystackRenewalPayment(int orderId, string Reference)
        {

            var orderitem = _dbContext.Orders.Where(c => c.Email == User.Identity.Name).SingleOrDefault(c => c.OrderId == orderId);

            decimal amount = (orderitem.Total * 100) + (orderitem.Total  * 100 * (decimal.Parse("0.05")));

            int payStackValue = Convert.ToInt32(amount);
            var result = await Task.Run(() => InitializeResponse(orderitem.Email, payStackValue));
            orderitem.Refinfo = result.Data.Reference;

            _dbContext.Update(orderitem);
            _dbContext.SaveChanges();


            if (orderitem == null)
            {
                return Json(new { isSuccess = false });
            }



            if (result.Status == true)
            {
                return Json(new { isSuccess = true, redirectUrl = result.Data.AuthorizationUrl });

            }
            else
            {
                return Json("An error occcured");

            }




        }


        public async Task<IActionResult> AnnualBilling()
        {


            var cart = _shoppingCart.GetCart(this.HttpContext);
            ShoppingCartViewModel model = new ShoppingCartViewModel();

            List<ProductOffering> products = new List<ProductOffering>();
            var items = await cart.GetCartItemsAsync();


            model.CartItems = items.ToList();
            model.CartTotal = (await cart.GetTotal() * 12);


            model.User = await _userManager.GetUserAsync(User);


            foreach (var item in model.CartItems)
            {

                products.Add(_dbContext.Products.FirstOrDefault(Opt => Opt.cspID.ToLower() == item.ProductId.ToLower()));

            }
            model.Products = products;
            ViewBag.BillingCycle = "Annual";
            return View(nameof(Index), model);
            // return View(model);
        }


        public async Task<IActionResult> VmLanding(string name, string productid, string skuid)
        {
            VmShoppingCartViewModel model = new VmShoppingCartViewModel
            {
                User = await _userManager.GetUserAsync(User),

                CartItems = name,

                Productid = productid,
                Skuid = skuid
            };

            var user = await _userManager.GetUserAsync(User);
            if (user.HasAgreement)
            {
                return View(model);
            }
            else
            {
                var agree = await _azureservice.ConfirmCloudAcceptance(user.CspId, user.FirstName, user.LastName, user.Email, user.PhoneNumber);
                if (agree != null)
                {
                    user.HasAgreement = true;
                    await _userManager.UpdateAsync(user);
                    return View(model);
                }
                return Content("Unable to Confirm Agreement");
            }
        }




      


        public TransactionInitializeResponse InitializeResponse(string Email, int amount)
        {
            var testOrLiveSecret = AppSettings.PayStackSecret;
            var api = new PayStackApi(testOrLiveSecret);
            var transaction = api.Transactions.Initialize(Email, amount);
            return transaction;

        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> PayStackCheckout(CustomerPaymentModel customerPaymentModel)
        {



            var newOrder = new Order();
            var updateNewOrder = await TryUpdateModelAsync(newOrder);

            decimal totalPayment;
            totalPayment = customerPaymentModel.payment;
            var identityUser = await _userManager.GetUserAsync(HttpContext.User);

            int payStackValue = Convert.ToInt32(totalPayment * 100);
            var result = await Task.Run(() => InitializeResponse(customerPaymentModel.Email, payStackValue));

            if (result.Status == true)
            {
                newOrder.OrderDate = DateTime.Now;
                newOrder.Total = totalPayment;
                newOrder.Refinfo = result.Data.Reference;
                newOrder.Phone = identityUser.PhoneNumber;
                //newOrder.PromoCode = customerPaymentModel.PromoCode;
                newOrder.Domain = customerPaymentModel.Domain;
                //     newOrder.FulFillmentDate = DateTime.Now;
                newOrder.BillingCycle = customerPaymentModel.BillingCycle;
                var id = newOrder.OrderId;

                //Save Order
                _dbContext.Orders.Add(newOrder);
                _dbContext.SaveChanges();

                //process Order
                var cart = _shoppingCart.GetCart(this.HttpContext);
                await cart.CreateOrderAsync(newOrder);


                return Json(new { isSuccess = true, redirectUrl = result.Data.AuthorizationUrl });

            }

            else
            {
                return Json("An error occcured");
            }
        }


        public async Task<IActionResult> OrderCompleted(string reference)
        {

            OrderCompletedHelper Helper = new OrderCompletedHelper(_emailservice, _dbContext);
            var testOrLiveSecret = AppSettings.PayStackSecret;
            var api = new PayStackApi(testOrLiveSecret);
            var verifyResponse = api.Transactions.Verify(reference);




            if (verifyResponse.Status)
            {



                var orderitem = _dbContext.Orders.Include(i => i.OrderDetails).SingleOrDefault(c => c.Refinfo == reference);

                
                    if (orderitem != null && orderitem.CspOrderId != null)
                    {
                        var CspOrderId = orderitem.CspOrderId;
                        var user = await _userManager.GetUserAsync(User);



                        if (CspOrderId != null)
                        {
                            var subscription = await _azureservice.GetSubscriptionbyOrderAsync(user.CspId, CspOrderId);
                            if (subscription != null)
                            {
                                foreach (var sub in subscription.Items)
                                {
                                    if (sub.Status.ToLower() == "suspended")
                                    {
                                        var reactivateSub = await _azureservice.ReactivateSubscriptionAsync(user.CspId, sub.Id);

                                        if (reactivateSub != null)
                                        {
                                            Console.WriteLine(reactivateSub.Status);
                                        }
                                    }
                                }
                            }
                        }

                        else
                        {
                            return Content("CSP order not Found");
                        }


                        orderitem.PaymentTransactionReference = verifyResponse.Data.Reference;
                        orderitem.FulfillPayment = true;
                        orderitem.FulFillmentDate = DateTime.Now;
                        orderitem.LastPaymentDate = DateTime.Now;
                        orderitem.PaymentDate = DateTime.Now;
                        orderitem.PaymentGateWay = Config.Paystack;


                    if (orderitem.BillingCycle.ToLower() == "monthly")
                        {
                            orderitem.NextPaymentDate = orderitem.LastPaymentDate.AddDays(30);

                        }

                        if (orderitem.BillingCycle.ToLower() == "annual")
                        {
                            orderitem.NextPaymentDate = orderitem.LastPaymentDate.AddYears(1);

                        }
                        if (orderitem.BillingCycle.ToLower() == "quarterly")
                        {
                            orderitem.NextPaymentDate = orderitem.LastPaymentDate.AddMonths(3);

                        }
                        if (orderitem.BillingCycle.ToLower() == "biannual")
                        {
                            orderitem.NextPaymentDate = orderitem.LastPaymentDate.AddMonths(6);

                        }
                        _dbContext.Update(orderitem);
                        _dbContext.SaveChanges();

                        // Todo send Notifications


                        return RedirectToAction(nameof(CustomerDashboardController.Orders), "CustomerDashboard");
                    }

                

                if (orderitem != null && orderitem.FulfillPayment == false)
                {
                    orderitem.Payment = true;
                    orderitem.PaymentDate = DateTime.Now;
                    orderitem.PaymentGateWay = Config.Paystack;
                    //   orderitem.PaymentDate = verifyTransaction.Data.DatePaid;
                    orderitem.PaymentTransactionReference = verifyResponse.Data.Reference;
                    _dbContext.SaveChanges();

                    var CSPUser = await _userManager.GetUserAsync(User);

                    // User 

                    if (CSPUser.CspId == null)
                    {
                        var profileCreationObject = Helper.BuildCustomerModel(CSPUser, orderitem);
                        var createCustomerCsp = await _cspApi.CreateCustomerAsync(profileCreationObject);
                        var applicationUser = await _userManager.GetUserAsync(User);

                        if (createCustomerCsp != null)
                        {
                            ///////// upadating user Identity


                            applicationUser.CspId = createCustomerCsp.CompanyProfile.TenantId;

                            applicationUser.Domain = createCustomerCsp.CompanyProfile.Domain;
                            applicationUser.BillingProfileId = createCustomerCsp.BillingProfile.Id;
                            applicationUser.Etag = createCustomerCsp.BillingProfile.Attributes.Etag;
                            applicationUser.CspDefaultUserName = createCustomerCsp.UserCredentials.UserName;
                            applicationUser.CspDefaultPassword = createCustomerCsp.UserCredentials.Password;
                            try
                            {
                                applicationUser.Url = createCustomerCsp.CompanyProfile.Links.Self.Uri;
                            }
                            catch
                            {


                            }
                            var result = await _userManager.UpdateAsync(applicationUser);




                            //  var messg = Helper.SendCustomerCreatedMail(applicationUser, createCustomerCsp);

                            var msg = _emailservice.SendNewCustomerCreated(applicationUser);

                          
                            // create order


                            var model = Helper.BuildOrderModel(orderitem, createCustomerCsp.CompanyProfile.TenantId);

                            


                            var CreateOrder = await _cspApi.CreateOrderAsync(createCustomerCsp.CompanyProfile.TenantId, model);
                            if (CreateOrder != null)
                            {
                                orderitem.FulfillPayment = true;
                                orderitem.FulFillmentDate = DateTime.Now;
                                orderitem.LastPaymentDate = DateTime.Now;

                                if (orderitem.BillingCycle.ToLower() == "annual")
                                {
                                    orderitem.NextPaymentDate = DateTime.Now.AddYears(1);
                                }
                                else if (orderitem.BillingCycle.ToLower() == "monthly")
                                {
                                    orderitem.NextPaymentDate = DateTime.Now.AddDays(30);
                                }
                                else if (orderitem.BillingCycle.ToLower() == "biannual")
                                {
                                    orderitem.NextPaymentDate = DateTime.Now.AddMonths(6);
                                }
                                else if (orderitem.BillingCycle.ToLower() == "quarterly")
                                {
                                    orderitem.NextPaymentDate = DateTime.Now.AddMonths(3);
                                }

                                orderitem.CspOrderId = CreateOrder.Id;
                                _dbContext.Update(orderitem);
                                _dbContext.SaveChanges();
                               


                                var message = _emailservice.SendOrderCompletedMail(applicationUser, orderitem);


                                //var message = _emailservice.SendOrderCompletedMail(applicationUser, orderitem);
                                ViewData["Message"] = "Congratulations, Your Products has successfully been provisioned on Microsoft CSP Platform. Please Login into your account administer your provisioned products. Check the video below for a quick onboarding guide.";
                                return View();

                            }

                            else
                            {
                                ViewData["Message"] = "An Error Occured Submitting your Order Please contact Support";
                                return View();
                            }



                            // Todo


                        }

                        else
                        {
                            ViewData["Message"] = "An error occured while provisioning User on CSP, Customer acccount cannot be created";

                            return View();
                        }





                    }

                    else
                    {

                       

                        var model = Helper.BuildOrderModel(orderitem, CSPUser.CspId);
                        var CreateOrder = await _cspApi.CreateOrderAsync(CSPUser.CspId, model);



                        if (CreateOrder != null)
                        {
                            orderitem.FulfillPayment = true;
                            orderitem.FulFillmentDate = DateTime.Now;
                            orderitem.LastPaymentDate = DateTime.Now;

                            if (orderitem.BillingCycle.ToLower() == "annual")
                            {
                                orderitem.NextPaymentDate = DateTime.Now.AddYears(1);
                            }
                            else if (orderitem.BillingCycle.ToLower() == "monthly")
                            {
                                orderitem.NextPaymentDate = DateTime.Now.AddDays(30);
                            }
                            else if (orderitem.BillingCycle.ToLower() == "biannual")
                            {
                                orderitem.NextPaymentDate = DateTime.Now.AddMonths(6);
                            }
                            else if (orderitem.BillingCycle.ToLower() == "quarterly")
                            {
                                orderitem.NextPaymentDate = DateTime.Now.AddMonths(3);
                            }
                            orderitem.CspOrderId = CreateOrder.Id;

                            _dbContext.SaveChanges();
                           

                            var message = _emailservice.SendOrderCompletedMail(CSPUser, orderitem);

                            ViewData["Message"] = "Congratulations, Your Products has successfully been provisioned on Microsoft CSP Platform. Please Login into your account administer your provisioned products. Check the video below for a quick onboarding guide.";
                            return View();

                        }

                        else
                        {
                            ViewData["Message"] = "An Error Occured Creating your Submitting your Order Please contact Admin";


                            return View();
                        }



                    }
                    //}

                    //else
                    //{
                    //    ViewData["Message"] = "Error your order payment couldn't be confirmed by the server";
                    //    return View();
                    //}
                }
                else
                {
                    ViewData["Message"] = "Error your order payment couldn't be confirmed by the server";
                    return View();
                }
            }

            ViewData["Message"] = "Error your order payment couldn't be confirmed by the server";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> FCHCheckout(CustomerPaymentModel customerPaymentModel)
        {



            var newOrder = new Order();
            var updateNewOrder = await TryUpdateModelAsync(newOrder);

            decimal totalPayment;
            totalPayment = customerPaymentModel.payment;
            var identityUser = await _userManager.GetUserAsync(HttpContext.User);


            if (customerPaymentModel.Reference.Contains("FCC"))
            {
                newOrder.OrderDate = DateTime.Now;
                newOrder.Total = totalPayment;
                newOrder.Refinfo = customerPaymentModel.Reference;
                newOrder.Phone = identityUser.PhoneNumber;
                newOrder.CompanyAddress = identityUser.CompanyAddress;
                //newOrder.PromoCode = customerPaymentModel.PromoCode;
                newOrder.Domain = customerPaymentModel.Domain;
                //       newOrder.FulFillmentDate = DateTime.Now;
                newOrder.BillingCycle = customerPaymentModel.BillingCycle;

                var id = newOrder.OrderId;

                //Save Order
                _dbContext.Orders.Add(newOrder);
                _dbContext.SaveChanges();

                //process Order
                var cart = _shoppingCart.GetCart(this.HttpContext);
                await cart.CreateOrderAsync(newOrder);


                return Json(new { isSuccess = true });

            }


            else
            {
                return Json("An error occcured");
            }
        }



        public async Task<IActionResult> FCHOrderCompleted(string transaction_reference)
        {

            var referenceid = transaction_reference;

            var verify = referenceid.Substring(0, 3);

            if (verify == "FCC")
            {
                var verifyTransaction = await _firstCheckout.TransactionVerification(transaction_reference);

                if (verifyTransaction != null && verifyTransaction.Data.Status == "Paid")
                {
                    OrderCompletedHelper Helper = new OrderCompletedHelper(_emailservice, _dbContext);
                    var orderitem = _dbContext.Orders.Include(i => i.OrderDetails).SingleOrDefault(c => c.Refinfo == transaction_reference);

                    if (orderitem != null && orderitem.FulfillPayment == false)
                    {
                        orderitem.Payment = true;
                        orderitem.PaymentGateWay = Config.FirstCheckout;
                        orderitem.PaymentDate = DateTime.Now;
                        orderitem.PaymentTransactionReference = verifyTransaction.Data.PaymentTransactionReference;
                        _dbContext.SaveChanges();

                        var CSPUser = await _userManager.GetUserAsync(User);

                        // User 

                        if (CSPUser.CspId == null)
                        {
                            var profileCreationObject = Helper.BuildCustomerModel(CSPUser, orderitem);
                            var createCustomerCsp = await _cspApi.CreateCustomerAsync(profileCreationObject);
                            var applicationUser = await _userManager.GetUserAsync(User);

                            if (createCustomerCsp != null)
                            {
                                ///////// upadating user Identity


                                applicationUser.CspId = createCustomerCsp.CompanyProfile.TenantId;

                                applicationUser.Domain = createCustomerCsp.CompanyProfile.Domain;
                                applicationUser.BillingProfileId = createCustomerCsp.BillingProfile.Id;
                                applicationUser.Etag = createCustomerCsp.BillingProfile.Attributes.Etag;
                                applicationUser.CspDefaultUserName = createCustomerCsp.UserCredentials.UserName;
                                applicationUser.CspDefaultPassword = createCustomerCsp.UserCredentials.Password;
                                try
                                {
                                    applicationUser.Url = createCustomerCsp.CompanyProfile.Links.Self.Uri;
                                }
                                catch
                                {


                                }


                                var agree = await _azureservice.ConfirmCloudAcceptance(createCustomerCsp.CompanyProfile.TenantId, applicationUser.FirstName, applicationUser.LastName, applicationUser.Email, applicationUser.PhoneNumber);


                                if (agree != null)
                                {
                                    applicationUser.HasAgreement = true;

                                }

                                var result = await _userManager.UpdateAsync(applicationUser);


                                //  var messg = Helper.SendCustomerCreatedMail(applicationUser, createCustomerCsp);

                                var msg = _emailservice.SendNewCustomerCreated(applicationUser);
                                

                                var model = Helper.BuildOrderModel(orderitem, createCustomerCsp.CompanyProfile.TenantId);

                               


                                var CreateOrder = await _cspApi.CreateOrderAsync(createCustomerCsp.CompanyProfile.TenantId, model);
                                if (CreateOrder != null)
                                {
                                    orderitem.FulfillPayment = true;
                                    orderitem.FulFillmentDate = DateTime.Now;
                                    orderitem.LastPaymentDate = DateTime.Now;
                                    if (orderitem.BillingCycle.ToLower() == "annual")
                                    {
                                        orderitem.NextPaymentDate = DateTime.Now.AddYears(1);
                                    }
                                    else
                                    {
                                        orderitem.NextPaymentDate = DateTime.Now.AddDays(30);
                                    }
                                    orderitem.CspOrderId = CreateOrder.Id;
                                    _dbContext.Update(orderitem);
                                    _dbContext.SaveChanges();
                                    //  var message = Helper.SendServiceProvisionedMail(applicationUser);
                                    var message = _emailservice.SendOrderCompletedMail(applicationUser, orderitem);
                                    ViewData["Message"] = "Congratulations, Your Products has successfully been provisioned on Microsoft CSP Platform. Please Login into your account administer your provisioned products. Check the video below for a quick onboarding guide.";
                                    return View(nameof(OrderCompleted));

                                }

                                else
                                {
                                    ViewData["Message"] = "An Error Occured Submitting your Order Please contact Support";
                                    return View(nameof(OrderCompleted));
                                }



                                // Todo


                            }

                            else
                            {
                                ViewData["Message"] = "An error occured while provisioning User on CSP, Customer acccount cannot be created";

                                return View(nameof(OrderCompleted));
                            }





                        }

                        else
                        {

                            

                            if (CSPUser.HasAgreement == false)
                            {

                                var agree = await _azureservice.ConfirmCloudAcceptance(CSPUser.CspId, CSPUser.FirstName, CSPUser.LastName, CSPUser.Email, CSPUser.PhoneNumber);


                                if (agree != null)
                                {
                                    CSPUser.HasAgreement = true;
                                    await _userManager.UpdateAsync(CSPUser);
                                }
                            }

                            var model = Helper.BuildOrderModel(orderitem, CSPUser.CspId);
                            var CreateOrder = await _cspApi.CreateOrderAsync(CSPUser.CspId, model);



                            if (CreateOrder != null)
                            {
                                orderitem.FulfillPayment = true;
                                orderitem.FulFillmentDate = DateTime.Now;
                                orderitem.LastPaymentDate = DateTime.Now;
                                if (orderitem.BillingCycle.ToLower() == "annual")
                                {
                                    orderitem.NextPaymentDate = DateTime.Now.AddYears(1);
                                }
                                else if (orderitem.BillingCycle.ToLower() == "monthly")
                                {
                                    orderitem.NextPaymentDate = DateTime.Now.AddDays(30);
                                }
                                else if (orderitem.BillingCycle.ToLower() == "biannual")
                                {
                                    orderitem.NextPaymentDate = DateTime.Now.AddMonths(6);
                                }
                                else if (orderitem.BillingCycle.ToLower() == "quarterly")
                                {
                                    orderitem.NextPaymentDate = DateTime.Now.AddMonths(3);
                                }
                                orderitem.CspOrderId = CreateOrder.Id;
                                _dbContext.Update(orderitem);
                                _dbContext.SaveChanges();
                                //   var message = Helper.SendServiceProvisionedMail(CSPUser);
                                var message = _emailservice.SendOrderCompletedMail(CSPUser, orderitem);

                                ViewData["Message"] = "Congratulations, Your Products has successfully been provisioned on Microsoft CSP Platform. Please Login into your account administer your provisioned products. Check the video below for a quick onboarding guide.";
                                return View(nameof(OrderCompleted));

                            }

                            else
                            {
                                ViewData["Message"] = "An Error Occured Creating your Submitting your Order Please contact Admin";
                                return View(nameof(OrderCompleted));
                            }



                        }
                    }

                    else
                    {
                        ViewData["Message"] = "Error your order payment couldn't be confirmed by the server";
                        return View(nameof(OrderCompleted));
                    }
                }
                else
                {
                    ViewData["Message"] = "Error your order payment couldn't be confirmed by the server";
                    return View(nameof(OrderCompleted));
                }
            }


            else if (verify == "RNW")
            {
                var RenewalverifyTransaction = await _firstCheckout.TransactionVerification(transaction_reference);


                if (RenewalverifyTransaction != null && RenewalverifyTransaction.Data.Status == "Paid")
                {

                    var orderitem = _dbContext.Orders.Include(i => i.OrderDetails).SingleOrDefault(c => c.Refinfo == transaction_reference);

                    var CspOrderId = orderitem.CspOrderId;
                    var user = await _userManager.GetUserAsync(User);



                    if (CspOrderId != null)
                    {
                        var subscription = await _azureservice.GetSubscriptionbyOrderAsync(user.CspId, CspOrderId);
                        if (subscription != null)
                        {
                            foreach (var sub in subscription.Items)
                            {
                                if (sub.Status.ToLower() == "suspended")
                                {
                                    var reactivateSub = await _azureservice.ReactivateSubscriptionAsync(user.CspId, sub.Id);

                                    if (reactivateSub != null)
                                    {
                                        Console.WriteLine(reactivateSub.Status);
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        return Content("CSP order not Found");
                    }


                    orderitem.PaymentTransactionReference = RenewalverifyTransaction.Data.PaymentTransactionReference;
                    orderitem.FulfillPayment = true;
                    orderitem.PaymentGateWay = Config.FirstCheckout;
                    orderitem.FulFillmentDate = DateTime.Now;
                    orderitem.LastPaymentDate = DateTime.Now;
                    orderitem.PaymentDate = DateTime.Now;
                    if (orderitem.BillingCycle.ToLower() == "monthly")
                    {
                        orderitem.NextPaymentDate = orderitem.LastPaymentDate.AddDays(30);

                    }

                    if (orderitem.BillingCycle.ToLower() == "annual")
                    {
                        orderitem.NextPaymentDate = orderitem.LastPaymentDate.AddYears(1);

                    }
                    if (orderitem.BillingCycle.ToLower() == "quarterly")
                    {
                        orderitem.NextPaymentDate = orderitem.LastPaymentDate.AddMonths(3);

                    }
                    if (orderitem.BillingCycle.ToLower() == "biannual")
                    {
                        orderitem.NextPaymentDate = orderitem.LastPaymentDate.AddMonths(6);

                    }
                    _dbContext.Update(orderitem);
                    _dbContext.SaveChanges();

                    // Todo send Notifications
                    // Activate sub if suspended


                    return RedirectToAction(nameof(CustomerDashboardController.Orders), "CustomerDashboard");
                }

            }





            else if (verify == "INT")
            {
                ViewData["Message"] = "Error your order payment couldn't be confirmed by the server";
                return View(nameof(OrderCompleted));
            }
            ViewData["Message"] = "Error your order payment couldn't be confirmed by the server";
            return View(nameof(OrderCompleted));
        }
        public IActionResult DomainChoice()
        {
            return View();
        }

        


    }
}