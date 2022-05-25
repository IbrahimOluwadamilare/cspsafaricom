using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cspv3.Services;
using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using cspv3.Models;
using cspv3.Data;
 
using System.Net.Http;
using cspv3.Helpers;
using Newtonsoft.Json;
using cspv3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using cspv3.Models.CspApiModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Net.Http.Headers;
using cspv3.Models.CspApiModels.License;
using NToastNotify;
using cspv3.Models.CspApiModels.UserResponse;

namespace cspv3.Controllers
{
    [Authorize]

    public class CustomerDashboardController : Controller
    {
        private ICustomerDashboard _customerDashboard;
        private ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly IAzureApi _azureservice;
        private readonly ICSPapi _cspApi;
        private readonly IEmailSender _emailSender;
        private readonly IShoppingCart _shoppingCart;
        private readonly IToastNotification _toastNotification;

        public CustomerDashboardController(UserManager<ApplicationUser> userManager, ICustomerDashboard customerDashboard, ApplicationDbContext context, IHostingEnvironment env, IAzureApi azureApi, IEmailSender emailSender, ICSPapi CspApi, IShoppingCart shoppingCart, IToastNotification toastNotification)
        {
            _customerDashboard = customerDashboard;
            _dbContext = context;
            _userManager = userManager;
            _env = env;
            _azureservice = azureApi;
            _cspApi = CspApi;
            _emailSender = emailSender;
            _shoppingCart = shoppingCart;
            _toastNotification = toastNotification;

        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            try
            {
                ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
                return View(applicationUser);


            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Account");
            }


        }

        public async Task<IActionResult> ControlDashboard()
        {
            //orders
            var user = await _userManager.GetUserAsync(User);

            var email = user.Email;
            AdminDashBoardViewModel adminViewModel = new AdminDashBoardViewModel
            {
                Order = _dbContext.Orders.AsEnumerable(),
                Product = _dbContext.Products.AsEnumerable()
            };
            var orderscost = adminViewModel.Order.Where(b => b.Email == email).Sum(x => x.Total);


            var fulfilled = adminViewModel.Order.Where(o => o.FulfillPayment == true && o.Email == email).Count();
          //  var totalorder = adminViewModel.Order.Where(b => b.Email == email).Sum(x => x.Total);
            var unfulfilled = adminViewModel.Order.Where(o => o.FulfillPayment == false && o.Email == email).Count();
            var payment = adminViewModel.Order.Where(o => o.Payment == true && o.Email == email).Count();
            var unpaid = adminViewModel.Order.Where(o => o.Payment == false && o.Email == email).Count();
            var inorder = adminViewModel.Order.Where(o => o.Refinfo == null && o.Email == email).Count();


            ViewBag.Orders = orderscost;
            ViewBag.fulfilled = fulfilled;
            ViewBag.unfulfilled = unfulfilled;
            ViewBag.payment = payment;
            ViewBag.unpaid = unpaid;
            ViewBag.inorder = inorder;



            ////subscriptions
            //var user = await _userManager.GetUserAsync(User);

            //string customerid = user.CspId;


            //var CustomerSubs = await _azureservice.GetCustomerSubscriptionsAsync(customerid);
            //var sub = CustomerSubs.Items.Where(x => x.EffectiveStartDate == x.EffectiveStartDate.AddDays(30)).Count();
            //ViewBag.ExpSub = sub;


            return View(adminViewModel);
        }

        // GET: /<controller>/
        public async Task<IActionResult> Profile()
        {
            try
            {
                ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
                return View(applicationUser);


            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Account");
            }

        }


        // GET: /<controller>/

        public async Task<IActionResult> SupportIndex(string sortOrder, int? page)
        {
            var dbOrders = await _customerDashboard.GetSupportsAsync(User.Identity.Name);
             
            var vieworders = dbOrders.Select(a => a);
            switch (sortOrder)
            {
                case "id_desc":
                    vieworders = vieworders.OrderByDescending(s => s.Id);
                    break;
                default:
                    vieworders = vieworders.OrderBy(s => s.Id);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(vieworders.ToList());


        }

        



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SupportAction(SupportViewModel support)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                var ticket = new Support
                {
                    
                    CaseOwner = User.Identity.Name,
                    Message = support.Message,
                    Priority = support.Priority,
                    DateCreated = DateTime.Now,
                    Subject = support.Subject,
                    Status = StatusType.Open,
                    Department = support.Department,
                    
                };
                //   var filePath = Path.GetTempFileName();
                if (support.File != null)
                {

                    ticket.Attachment = support.File.Name;
                    //   var filePath = Path.Combine(_env.WebRootPath, ("SupportTicketFiles\\" + support.File.FileName));
                      var path = Path.Combine(_env.WebRootPath, ("SupportTicketFiles\\" + support.File.FileName));

                    if (support.File.Length > 0)
                    {
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await support.File.CopyToAsync(stream);
                        }
                    }
                }
                _dbContext.SupportTicket.Add(ticket);
                _dbContext.SaveChanges();

                var sendmail = _emailSender.SendSupportTicket(user, ticket);

                _toastNotification.AddSuccessToastMessage("Ticket Submitted", new NotyOptions() { Theme = "metroui", ProgressBar = false });
                return RedirectToAction(nameof(SupportIndex));
            }
            else
            {
                return View(nameof(Support));
            }

        }

        public IActionResult Support()
        {
            return View();
        }
        [Route("customerdashboard/supportdetail")]

        public IActionResult SupportDetail()
        {
            var support = _dbContext.SupportTicket.Where(s => s.CaseOwner == User.Identity.Name).ToList();
            return View();
        }

        public IActionResult SupportDetails(int id)
        {
            var support = _dbContext.SupportTicket.FirstOrDefault(s => s.Id == id);
            return View(support);
        }

        [Route("customerdashboard/orderdetails")]

        public async Task<IActionResult> OrderDetails(int id)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel
            {
                OrderDetails = await _customerDashboard.GetOrderDetailsAsync(id),
                Products = _dbContext.Products.AsEnumerable()
            };
            return View(model);
        }

        [Route("customerdashboard/orders")]
        public async Task<IActionResult> Orders()
        {
            var user = await _userManager.GetUserAsync(User);
            var orderdata = await _customerDashboard.GetOrdersAsync(user.Email);
          
            return View( orderdata);
        }

           [Route("customerdashboard/fulfilledorders")]
        public async Task<IActionResult> FulFilledOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var orderdata = await _customerDashboard.GetFulfilledOrdersAsync(user.Email);
          
            return View( orderdata);
        }



        [Route("customerdashboard/subscriptions")]
        public async Task<IActionResult> Subscriptions()
        {
           
            var user = await _userManager.GetUserAsync(User);

            string customerid = user.CspId;

            if (customerid == null)
            {
                return Content("Customer is not Registered on the platform");
            }

            try
            {
                var CustomerSubs = await _azureservice.GetCustomerSubscriptionsAsync(customerid);


                return View(CustomerSubs.Items.ToList());
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult ManageProducts()
        {
            return View();
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


        public async Task<IActionResult> GenerateInvoice(int orderid)
        {
            var order = await _customerDashboard.GetOrderbyId(orderid);
                      
            return View(order);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                model.UsageLocation = "US";
                model.PasswordProfile = new PasswordProfile() { ForceChangePassword = true, Password = "Password!1" };

                model.UserPrincipalName = model.UserPrincipalName + "@" + user.Domain;

                var customerUser = await _cspApi.CreateuserforcustomerAsync(model, user.CspId);
                _toastNotification.AddSuccessToastMessage(model.UserPrincipalName + " created", new NotyOptions() { Text = "User Created", Theme= "metroui" });

                return RedirectToAction(nameof(CustomerUsers));
            }
            else
            {
                return View(nameof(CustomerUsers));
            }

        }

        public IActionResult CreateUser(string customerid)
        {

            if (customerid == null)
            {
                _toastNotification.AddErrorToastMessage("No License Found, Cannot Create User", new NotyOptions() { Theme = "metroui", ProgressBar = false });
                return RedirectToAction(nameof(CustomerUsers));
            }
            ViewBag.CustomerId = customerid;

            return View();
        }

        public async Task<IActionResult> CustomerUsers()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.CspId != null)
            {

                var users = await _cspApi.GetcustomerUsersAsync(user.CspId);

                if (users == null)
                {

                    var response = new List<Models.CspApiModels.UserResponse.Item>();
                    _toastNotification.AddErrorToastMessage("No Registered User", new NotyOptions() { Theme = "metroui", ProgressBar = false });

                    ViewBag.CustomerId = null;
                    return View(response);
                }

                ViewBag.CustomerId = user.CspId;

                return View(users.Items);
            }

            else
            {
                var response = new List<Models.CspApiModels.UserResponse.Item>();
                _toastNotification.AddInfoToastMessage("No Registered User", new NotyOptions() { Theme = "metroui", ProgressBar = false });

                ViewBag.CustomerId = null;
                return View(response);
            }

        }
       public async Task<IActionResult> DeletedCustomerUsers()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.CspId != null)
            {

                var users = await _cspApi.GetDeletedcustomerUsersAsync(user.CspId);

                if (users == null)
                {

                    var response = new List<Models.CspApiModels.UserResponse.Item>();
                    _toastNotification.AddErrorToastMessage("No Registered User", new NotyOptions() { Theme = "metroui", ProgressBar = false });

                    ViewBag.CustomerId = null;
                    return View(response);
                }

                ViewBag.CustomerId = user.CspId;

                return View(users.Items);
            }

            else
            {
                _toastNotification.AddErrorToastMessage("No Deleted User found", new NotyOptions() { Theme = "metroui", ProgressBar = false });
                return RedirectToAction(nameof(CustomerUsers));
            }

        }

        public async Task<IActionResult> AddLicense(string UserId)
        {
            var subList = new List<SelectListItem>();
            var customer = await _userManager.GetUserAsync(User);

            var subs = await _cspApi.GetSubscribedSkuAsync(customer.CspId);
            foreach (var sub in subs.Items)
            {
                subList.Add(new SelectListItem { Text = sub.ProductSku.Name, Value = sub.ProductSku.Id });
            }
            if (subList.Count  < 1 )
            {
                _toastNotification.AddErrorToastMessage("No Subscription found for customer", new NotyOptions() { Theme = "metroui", ProgressBar = false });

                return RedirectToAction(nameof(CustomerUsers));
            }

            ViewBag.Subs = subList;
            ViewBag.UserId = UserId;

            return View();
        }
           public async Task<IActionResult> CustomerLicenses()
        {
            var customer = await _userManager.GetUserAsync(User);

            var licenses = await _cspApi.GetSubscribedSkuAsync(customer.CspId);

            if (licenses == null)
            {
                 licenses = new SubscribedSku();
               
            }
          

            return View(licenses);
        }
           public async Task<IActionResult> RestoreUser(string UserId)
        {
            var customer = await _userManager.GetUserAsync(User);

            var restore = await _cspApi.RestoreUserforcustomerAsync(customer.CspId, UserId);

            if (restore != null)
            {
                _toastNotification.AddInfoToastMessage("User Restored", new NotyOptions() { Theme = "metroui", ProgressBar = false });
                return RedirectToAction(nameof(CustomerUsers));             
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Unable to restore user", new NotyOptions() { Theme = "metroui", ProgressBar = false });
                return Content("Unable to restore user");

            }

        }


        [ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<IActionResult> AddLicense(AddLicenseViewModel model)
        {
            var customer = await _userManager.GetUserAsync(User);

            var licenses = await _cspApi.AssignLicense(model.SkuId, customer.CspId, model.UserId);

            if (licenses != null)
            {
                //   ViewBag.LicenseAssigned = "True";
                //   return View();
                return RedirectToAction(nameof(ViewLicense), new { model.UserId });
            }
            else
            {
                //  ViewBag.LicenseAssigned = "False";
                //  return View();
                return RedirectToAction(nameof(ViewLicense), new { model.UserId });
            }
                

        }
        public async Task<IActionResult> ViewLicense(string UserId)
        {
           

            var customer = await _userManager.GetUserAsync(User);
            var licenses = await _cspApi.GetLicensesForUser(customer.CspId, UserId);
            if (licenses != null)
            {
                return View(licenses.Items.ToList());

            }

            return RedirectToAction(nameof(CustomerUsers));

        }
        public async Task<IActionResult> DeleteUser(string UserId)
        {
            var customer = await _userManager.GetUserAsync(User);

            if (UserId == null)
            {
                return NotFound();
            }

            var user = await _cspApi.GetcustomerUserbyIdAsync(customer.CspId, UserId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

       
     

        [HttpPost/*, ActionName("DeleteUser")*/]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string UserId)
        {
            var customer = await _userManager.GetUserAsync(User);
            var customerUser = await _cspApi.DeleteUserforCustomerAsync(customer.CspId, UserId);
                    _toastNotification.AddWarningToastMessage("User Deleted", new NotyOptions() {  Theme = "metroui", ProgressBar = false});
                return RedirectToAction(nameof(CustomerUsers));

          
        }
        [HttpGet]
        [Route("ExportUsers")]
        public async Task<IActionResult> ExportUsers()
        {
            string rootFolder = _env.WebRootPath;
            string fileName = @"ExportUsers.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(rootFolder, fileName));
            }

            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(file))
            {
                var user = await _userManager.GetUserAsync(User);

                if (user.CspId != null)
                {

                    var users = await _cspApi.GetcustomerUsersAsync(user.CspId);



                   var traineeList = users.Items;

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("User");
                  
                    using (var cells = worksheet.Cells[1, 1, 1, 10]) //(1,1) (1,5)
                    {
                        cells.Style.Font.Bold = true;
                    }

                    int totalRows = traineeList.Count();

                    worksheet.Cells[1, 1].Value = "First Name";
                    worksheet.Cells[1, 2].Value = "Last Name";
                    worksheet.Cells[1, 3].Value = "ID";
                    worksheet.Cells[1, 4].Value = "Email";
                    worksheet.Cells[1, 5].Value = "User Principal Name";
                    worksheet.Cells[1, 6].Value = "State";
                   
                   
                    int i = 0;
                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        worksheet.Cells[row, 1].Value = traineeList[i].FirstName;
                        worksheet.Cells[row, 2].Value = traineeList[i].LastName;
                        worksheet.Cells[row, 3].Value = traineeList[i].Id;
                        worksheet.Cells[row, 4].Value = traineeList[i].UserPrincipalName;
                        worksheet.Cells[row, 5].Value = traineeList[i].DisplayName;
                        worksheet.Cells[row, 6].Value = traineeList[i].State;
                     
                        i++;
                    }

                    package.Save();
                }
            }

            var result = PhysicalFile(Path.Combine(rootFolder, fileName), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
            {
                FileName = file.Name
            }.ToString();

            return result;
        }
       
        [HttpGet]
        [Route("ExportSupport")]
        public async Task<IActionResult> ExportSupport()
        {
            string rootFolder = _env.WebRootPath;
            string fileName = @"ExportSupport.csv";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(rootFolder, fileName));
            }

            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(file))
            {
                var user = await _userManager.GetUserAsync(User);
                IList<Support> traineeList = _dbContext.SupportTicket.Where(t => t.CaseOwner == user.Email).ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("User");
                using (var cells = worksheet.Cells[1, 1, 1, 11]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                int totalRows = traineeList.Count();

                worksheet.Cells[1, 1].Value = "Department";
                worksheet.Cells[1, 2].Value = "Priority";
                worksheet.Cells[1, 3].Value = "ID";
                worksheet.Cells[1, 4].Value = "Subject";
                worksheet.Cells[1, 5].Value = "Message";
                worksheet.Cells[1, 6].Value = "Status";
                worksheet.Cells[1, 7].Value = "Date Created";
                worksheet.Cells[1, 8].Value = "Date Resolved";
                worksheet.Cells[1, 9].Value = "Case Owner";
                worksheet.Cells[1, 10].Value = "Attachment";
                worksheet.Cells[1, 11].Value = "Response";
              
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = traineeList[i].Department;
                    worksheet.Cells[row, 2].Value = traineeList[i].Priority;
                    worksheet.Cells[row, 3].Value = traineeList[i].Id;
                    worksheet.Cells[row, 4].Value = traineeList[i].Subject;
                    worksheet.Cells[row, 5].Value = traineeList[i].Message;
                    worksheet.Cells[row, 6].Value = traineeList[i].Status;
                    worksheet.Cells[row, 7].Value = traineeList[i].DateCreated.Date.ToString();
                    worksheet.Cells[row, 8].Value = traineeList[i].DateResolved.Date.ToString();
                    worksheet.Cells[row, 9].Value = traineeList[i].CaseOwner;
                    worksheet.Cells[row, 10].Value = traineeList[i].Attachment;
                    worksheet.Cells[row, 11].Value = traineeList[i].Response;
                    i++;
                }

                package.Save();

            }

            var result = PhysicalFile(Path.Combine(rootFolder, fileName), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
            {
                FileName = file.Name
            }.ToString();

            return result;
        }

        [HttpGet]
        [Route("ExportOrders")]
        public async Task<IActionResult> ExportOrders()
        {
            string rootFolder = _env.WebRootPath;
            string fileName = @"ExportOrders.csv";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(rootFolder, fileName));
            }

            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(file))
            {
                var user = await _userManager.GetUserAsync(User);
                IList<Order> traineeList = _dbContext.Orders.Where(t => t.Email == user.Email).ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("User");
                using (var cells = worksheet.Cells[1, 1, 1, 11]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                int totalRows = traineeList.Count();

                worksheet.Cells[1, 1].Value = "First Name";
                worksheet.Cells[1, 2].Value = "Last Name";
                worksheet.Cells[1, 3].Value = "Company Address";
                worksheet.Cells[1, 4].Value = "Country";
                worksheet.Cells[1, 5].Value = "Phone";
                worksheet.Cells[1, 6].Value = "Email";
                worksheet.Cells[1, 7].Value = "Total ";
                worksheet.Cells[1, 8].Value = "Payment";
                worksheet.Cells[1, 9].Value = "Domain";
                worksheet.Cells[1, 10].Value = "Payment Fufilled";
                worksheet.Cells[1, 11].Value = "Order Date";
                worksheet.Cells[1, 12].Value = "Reference Information";
                worksheet.Cells[1, 13].Value = "PromoCode ";
                worksheet.Cells[1, 14].Value = "OrderDetails";
                worksheet.Cells[1, 15].Value = "Path ";
                worksheet.Cells[1, 16].Value = "AuthorizationPayment ";
                worksheet.Cells[1, 17].Value = "FulFillmentDate";
                worksheet.Cells[1, 18].Value = "Order ID";

                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = traineeList[i].FirstName;
                    worksheet.Cells[row, 2].Value = traineeList[i].LastName;
                    worksheet.Cells[row, 3].Value = traineeList[i].CompanyAddress;
                    worksheet.Cells[row, 4].Value = traineeList[i].Country;
                    worksheet.Cells[row, 5].Value = traineeList[i].Phone;
                    worksheet.Cells[row, 6].Value = traineeList[i].Email;
                    worksheet.Cells[row, 7].Value = traineeList[i].Total;
                    worksheet.Cells[row, 8].Value = traineeList[i].Payment;
                    worksheet.Cells[row, 9].Value = traineeList[i].Domain;
                    worksheet.Cells[row, 10].Value = traineeList[i].FulfillPayment;
                    worksheet.Cells[row, 11].Value = traineeList[i].OrderDate.Date.ToString();
                    worksheet.Cells[row, 12].Value = traineeList[i].Refinfo;
                    worksheet.Cells[row, 13].Value = traineeList[i].PromoCode;
                    worksheet.Cells[row, 14].Value = traineeList[i].OrderDetails;
                    worksheet.Cells[row, 15].Value = traineeList[i].Path;
                    worksheet.Cells[row, 16].Value = traineeList[i].AuthorizationPayment;
                    worksheet.Cells[row, 17].Value = traineeList[i].FulFillmentDate.Date.ToString();
                    worksheet.Cells[row, 18].Value = traineeList[i].OrderId;

                    i++;
                }

                package.Save();

            }

            var result = PhysicalFile(Path.Combine(rootFolder, fileName), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
            {
                FileName = file.Name
            }.ToString();

            return result;
        }
        [HttpGet]
        [Route("ExportSubscription")]
        public async Task<IActionResult> ExportSubscription()
        {
            string rootFolder = _env.WebRootPath;
            string fileName = @"ExportSubscription.csv";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(rootFolder, fileName));
            }

            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(file))
            {
                var user = await _userManager.GetUserAsync(User);

                string customerid = user.CspId;
                if (customerid == null)
                {
                    return Content("Customer is not Registered on the platform");
                }

                if (customerid != null)
                {
                    var CustomerSubs = await _azureservice.GetCustomerSubscriptionsAsync(customerid);


                    var traineeList = CustomerSubs.Items;

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("User");
                    using (var cells = worksheet.Cells[1, 1, 1, 11]) //(1,1) (1,5)
                    {
                        cells.Style.Font.Bold = true;
                    }

                    int totalRows = traineeList.Count();

                    worksheet.Cells[1, 1].Value = "Order ID";
                    worksheet.Cells[1, 2].Value = "Subscription ID";
                    worksheet.Cells[1, 3].Value = "Name";
                    worksheet.Cells[1, 4].Value = "Quantity";
                    worksheet.Cells[1, 5].Value = "Billing Cycle";
                    worksheet.Cells[1, 6].Value = "Status";
                    worksheet.Cells[1, 7].Value = "Date Created";
                    worksheet.Cells[1, 8].Value = "Effective Start Date";
                    worksheet.Cells[1, 9].Value = "Auto Renewal Enabled";
                    worksheet.Cells[1, 10].Value = "Subscription End date";
                    worksheet.Cells[1, 11].Value = "Billing Type";
                    worksheet.Cells[1, 12].Value = "Offer Name ";

                    int i = 0;
                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        worksheet.Cells[row, 1].Value = traineeList[i].OrderId;
                        worksheet.Cells[row, 2].Value = traineeList[i].Id;
                        worksheet.Cells[row, 3].Value = traineeList[i].FriendlyName;
                        worksheet.Cells[row, 4].Value = traineeList[i].Quantity;
                        worksheet.Cells[row, 5].Value = traineeList[i].BillingCycle;
                        worksheet.Cells[row, 6].Value = traineeList[i].Status;
                        worksheet.Cells[row, 7].Value = traineeList[i].CreationDate.Date.ToString();
                        worksheet.Cells[row, 8].Value = traineeList[i].EffectiveStartDate.Date.ToString();
                        worksheet.Cells[row, 9].Value = traineeList[i].AutoRenewEnabled;
                        worksheet.Cells[row, 10].Value = traineeList[i].CommitmentEndDate;
                        worksheet.Cells[row, 11].Value = traineeList[i].BillingType;
                        worksheet.Cells[row, 12].Value = traineeList[i].OfferName;
                        i++;
                    }


                    package.Save();
                }

                
            }

            var result = PhysicalFile(Path.Combine(rootFolder, fileName), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            Response.Headers["Content-Disposition"] = new ContentDispositionHeaderValue("attachment")
            {
                FileName = file.Name
            }.ToString();

            return result;
        }
    }


}
