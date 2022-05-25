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
using System.Net.Http.Headers;
using OfficeOpenXml;
using PayStack.Net;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using cspv3.Models.BundleModels;

namespace cspv3.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdministratorDashboardController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly ICSPapi _cspApi;
        private IEmailSender _emailservice;
        private AppSettings AppSettings { get; set; }

        public IExcelToProductService _exceltoproduct { get; }

        private IAdministratorDashboard _administratorDashboard;
        private ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAzureApi _azureservice;

        public AdministratorDashboardController(UserManager<ApplicationUser> userManager, IAdministratorDashboard administratorDashboard, ApplicationDbContext context, IHostingEnvironment env, IExcelToProductService excelToProductService, ICSPapi CspApi, IOptions<AppSettings> settings, IEmailSender emailSender, IAzureApi azureservice)
        {
            _administratorDashboard = administratorDashboard;
            _dbContext = context;
            _userManager = userManager;
            _env = env;
            _exceltoproduct = excelToProductService;
            _cspApi = CspApi;
            AppSettings = settings.Value;
            _emailservice = emailSender;
            _azureservice = azureservice;
           
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var userid = HttpContext.Session.GetString("Username");
            var email = HttpContext.Session.GetString("Email");
            AdminDashBoardViewModel adminViewModel = new AdminDashBoardViewModel
            {
                Order = _dbContext.Orders.AsEnumerable(),
                Product = _dbContext.Products.AsEnumerable()
            };

            
          
            return View(adminViewModel);



        }
        public IActionResult SupportDetails(int id)
        {
            var tickets = _dbContext.SupportTicket.FirstOrDefault(key => key.Id == id);
            return View(tickets);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(UserModel model, string customerid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                model.UsageLocation = "US";
                model.PasswordProfile = new PasswordProfile() { ForceChangePassword = true, Password = "Password!1" };

                model.UserPrincipalName = model.UserPrincipalName + "@" + user.Domain;

                var customerUser = await _cspApi.CreateuserforcustomerAsync(model, user.CspId);

                if (customerUser != null)
                {

                }

                return RedirectToAction(nameof(CustomerUsers), new { id = customerid });
            }
            else
            {
                return View(nameof(Support));
            }

        }

        public IActionResult CreateUser(string customerid)
        {
            ViewBag.CustomerId = customerid;
            return View();
        }

        [Route("administratordashboard/report")]
        public IActionResult Report()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }

        public async Task<IActionResult> DeleteUser(string UserId, string CustomerId)
        {
            var customerUser = await _cspApi.DeleteUserforCustomerAsync(CustomerId, UserId);

            return RedirectToAction(nameof(CustomerUsers), new { id = CustomerId });



        }

        public async Task<IActionResult> GetLicenses(string UserId, string CustomerId)
        {
            var customerUser = await _cspApi.GetLicensesForUser(CustomerId, UserId);



            return RedirectToAction(nameof(CustomerUsers), new { id = CustomerId });



        }

        [ValidateAntiForgeryToken]

        [HttpPost]
        public IActionResult UpdateStatus(Support support)
        {
            var ticketing = _dbContext.SupportTicket.FirstOrDefault(key => key.Id == support.Id);
            if (support.Status == StatusType.Closed || support.Status == StatusType.Resolved)
            {
                support.DateResolved = DateTime.Now;
            }
            ticketing.Status = support.Status;
            ticketing.DateResolved = support.DateResolved;
            ticketing.Response = support.Response;



            _dbContext.Attach(ticketing).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(SupportIndex));
        }


        public async Task<IActionResult> SupportIndex(string sortOrder, int? page)
        {
            var dbOrders = await _dbContext.SupportTicket.ToListAsync();

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
        public async Task<IActionResult> HelpRequests(int? page)
        {
            var dbOrders = await _dbContext.HelpRequests.ToListAsync();

            var vieworders = dbOrders.Select(a => a);
       

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(vieworders.ToList());


        }

        //public async Task<IActionResult> GenerateInvoice()
        //{

        //    Models.InvoiceCustomerModel model = new Models.InvoiceCustomerModel();
        //    List<OrderDetail> details = new List<OrderDetail>();
        //    InvoiceViewModel Ordermodel = new InvoiceViewModel
        //    {
        //        Orders = await _administratorDashboard.GetOrdersAsync(),
        //        Products = await _dbContext.Products.ToListAsync()
        //    };


        //    foreach (var item in Ordermodel.Orders)
        //    {

        //        model.CompanyAddress = item.CompanyAddress;
        //        model.Email = item.Email;
        //        model.FirstName = item.FirstName;
        //        model.LastName = item.LastName;
        //        model.Phone = item.Phone;
        //        model.PromoCode = item.PromoCode;
        //        model.CartTotal = item.Total;
        //        details = _dbContext.OrderDetails.Where(c => c.OrderId == item.OrderId).ToList();
        //    }



        //    foreach (var item in details)
        //    {
        //        var productdetails = Ordermodel.Products.FirstOrDefault(d => d.cspID == item.ProductId);
        //        model.Notes = productdetails.Name + item.Quantity + item.UnitPrice;


        //    }

        //    //  var view = new ViewAsPdf("GenerateInvoice", model);


        //    return _administratorDashboard.GenerateInvoice("GenerateInvoice", model);
        //}

        //public async Task<IActionResult> DeleteAccount(string id)
        //{
        //    if (!String.IsNullOrEmpty(id))
        //    {
        //        ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
        //        if (applicationUser != null)
        //        {
        //            IdentityResult result = await _userManager.DeleteAsync(applicationUser);
        //            if (result.Succeeded)
        //            {

        //                return RedirectToAction("ManageCustomers");
        //            }
        //        }
        //    }
        //    return View();
        //}


        




        [Route("administratordashboard/fulfilledorders")]
        public async Task<IActionResult> FulfilledOrders(string sortOrder, int? page)
        {
            var dbOrders = await _administratorDashboard.GetOrdersAsync();

            var vieworders = dbOrders.Select(a => a);
            switch (sortOrder)
            {
                case "Orderid_desc":
                    vieworders = vieworders.OrderByDescending(s => s.OrderId);
                    break;
                default:
                    vieworders = vieworders.OrderBy(s => s.OrderId);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var orders = vieworders.Where(a => a.FulfillPayment == true).ToList();

            return View("ManageOrders", orders);


        }
         [Route("administratordashboard/allorders")]
        public async Task<IActionResult> AllOrders(string sortOrder, int? page)
        {
            var dbOrders = await _administratorDashboard.GetOrdersAsync();

       

            return View ("ManageOrders",  dbOrders.ToList());


        }
        
        [Route("administratordashboard/ManageCustomers")]
        public async Task<IActionResult> ManageCustomers(string sortOrder, int? page)
        {
            var vieworders = await _administratorDashboard.GetRegisteredUsersAsync();

            switch (sortOrder)
            {
                case "CustomerId_desc":
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


        [Route("administratordashboard/customerusers")]
        public async Task<IActionResult> CustomerUsers(int? page, string id)
        {
            if (id != null)
            {

                var users = await _cspApi.GetcustomerUsersAsync(id);

            


                ViewBag.CustomerId = id;

                return View(users.Items.ToList());
            }

            else
            {
                return RedirectToAction(nameof(Index));
            }

        }

        public IActionResult OrderDetails(int id)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel
            {
                OrderDetails = _administratorDashboard.OrderDetails(id),
                Products = _dbContext.Products.AsEnumerable()
            };
            return View(model);
        }

        public IActionResult ManageProducts()
        {
            return View();
        }
      
        public async Task<IActionResult> ManageBundles()
        {
            
           var bundles =  await _dbContext.Bundles.ToListAsync();
           // List<Bundle> model = new List<Bundle>();
            //model = _dbContext.Bundles.Select(r => new Bundle
            //{
            //    Id= r.Id,
            //    BundleName = r.BundleName,
               
            //}).ToList();
           return View(bundles);
        }

        public IActionResult CreateBundle()
        {
            return View();
        }
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateBundle(BundleVM datasource)
        {
            try
            {
                var newbundle = new Bundle
                {
                    BundleName = datasource.BundleName
                };
                _dbContext.Add(newbundle);

                foreach (var item in datasource.BundleCategory)
                {
                    var bundlecat = new BundleCategory
                    {
                        Bundle = newbundle,
                        CategoryName = item.CategoryName,
                        CspId = item.CspId,
                        Price = item.Price,


                    };
                    _dbContext.Add(bundlecat);

                }
                _dbContext.SaveChanges();
               
                return Json(new { isSuccess = true, message = "", redirecturl = Url.Action(nameof(ManageBundles)) });

            }
            catch (Exception)
            {

                return Json(new { isSuccess = false });
            }
        }

        
        
        
        [ValidateAntiForgeryToken]
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> ProductUpload(IFormFile file)
        {


            if (file == null)
                return Content("Argument null");

            //var mimetype = MimeMapping.MimeTypes.GetMimeMapping(file.FileName);
            //if (mimetype != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            //{
            //    return Content("Invalid Content Type");

            //}
            var filePath = Path.Combine(_env.WebRootPath, ("productfiles\\" + file.FileName));

            if (file.Length > 0)
            {
                if (System.IO.File.Exists(filePath))
                {

                    System.IO.File.Delete(filePath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                await _exceltoproduct.ConvertFileToProductString(filePath);
            }
            return RedirectToAction("Index");

        }
        public IActionResult ConfirmFulfill(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == id);
            return View(order);
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FulFillOrder(int id)
        {
            var usermodel = await _userManager.GetUserAsync(User);

            var Customer = new Customer
            {
                City = usermodel.City,
                CompanyAddress = usermodel.CompanyAddress,
                CompanyName = usermodel.CompanyName,
                Country = usermodel.Country,
                CspId = usermodel.CspId,
                Email = usermodel.Email,
                EmailConfirmed = usermodel.EmailConfirmed,
                FirstName = usermodel.FirstName,
                Id = usermodel.Id,
                LastName = usermodel.LastName,
                Level = usermodel.Level,
                PhoneNumber = usermodel.PhoneNumber,
                RegistrationDate = usermodel.RegisterationDate,
                State = usermodel.State,
                Url = usermodel.Url,
                UserAddress = usermodel.UserAddress,
                UserName = usermodel.UserName

            };
            ViewBag.userdetails = Customer;

            var orderitem = _dbContext.Orders.SingleOrDefault(c => c.OrderId == id);



            OrderCompletedHelper Helper = new OrderCompletedHelper(_emailservice, _dbContext);
            var testOrLiveSecret = AppSettings.PayStackSecret;
            var api = new PayStackApi(testOrLiveSecret);

            var verifyResponse = api.Transactions.Verify(orderitem.Refinfo);
            if (verifyResponse.Status && verifyResponse.Data.GatewayResponse.ToLower() == "successful")
            {
                var CSPUser = await _userManager.FindByEmailAsync(orderitem.Email);

                if (CSPUser == null || CSPUser.CspId == null)
                {
                    return Content("Customer Account Not Created");
                }
                var model = Helper.BuildOrderModel(orderitem, CSPUser.CspId);
                var CreateOrder = await _cspApi.CreateOrderAsync(CSPUser.CspId, model);


                if (CreateOrder != null)
                {
                    
                    orderitem.FulfillPayment = true;
                    _dbContext.SaveChanges();
                   // var message = Helper.SendServiceProvisionedMail(CSPUser);
                    var message = _emailservice.SendOrderCompletedMail(CSPUser, orderitem);



                    return View();

                }

                else
                {
                    ViewData["Message"] = "An Error Occured Creating your Submitting your Order Please contact Admin";
                    return Content("An Error Occured, Please Try again");
                }


              
            }
            else
            {
                return Content("User Did not Complete Payment");
            }

                

            return View();
        }


        ////upload reserved VM
        //[HttpPost("UploadFiles")]
        //public async Task<IActionResult> ReservedVMUpload(List<IFormFile> files)
        //{
        //    long size = files.Sum(f => f.Length);

        //    // full path to file in temp location
        //    var filePath = Path.GetTempFileName();

        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }

        //    return RedirectToAction("Index"); //or to wherever
        //}

        ////Uplaod Wragby Bundle
        //[HttpPost("UploadFiles")]
        //public async Task<IActionResult> WragbyBundleUpload(List<IFormFile> files)
        //{
        //    long size = files.Sum(f => f.Length);

        //    // full path to file in temp location
        //    var filePath = Path.GetTempFileName();

        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }

        //    return RedirectToAction("Index"); //or to wherever
        //}
        //#endregion

        [HttpGet]
        [Route("ExportAllUsers")]
        public async Task<IActionResult> ExportAllUsers()
        {
            string rootFolder = _env.WebRootPath;
            string fileName = @"ExportAllUsers.xlsx";
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
                IList<ApplicationUser> traineeList = _dbContext.Users.ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("User");
                using (var cells = worksheet.Cells[1, 1, 1, 10]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                int totalRows = traineeList.Count();

                worksheet.Cells[1, 1].Value = "First Name";
                worksheet.Cells[1, 2].Value = "Last Name";
                worksheet.Cells[1, 3].Value = "Trainee ID";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Company Name";
                worksheet.Cells[1, 6].Value = "Company Address";
                worksheet.Cells[1, 7].Value = "User Address";
                worksheet.Cells[1, 8].Value = "Registration Date";
                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = traineeList[i].FirstName;
                    worksheet.Cells[row, 2].Value = traineeList[i].LastName;
                    worksheet.Cells[row, 3].Value = traineeList[i].Id;
                    worksheet.Cells[row, 4].Value = traineeList[i].Email;
                    worksheet.Cells[row, 5].Value = traineeList[i].CompanyName;
                    worksheet.Cells[row, 6].Value = traineeList[i].CompanyAddress;
                    worksheet.Cells[row, 7].Value = traineeList[i].CompanyAddress;
                    worksheet.Cells[row, 8].Value = traineeList[i].RegisterationDate.ToString();
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
        [Route("ExportAllSupports")]
        public IActionResult ExportAllSupports()
        {
            string rootFolder = _env.WebRootPath;
            string fileName = @"ExportAllSupports.csv";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(rootFolder, fileName));
            }

            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(file))
            {

                IList<Support> traineeList = _dbContext.SupportTicket.ToList();

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
        [Route("ExportAllOrders")]
        public async Task<IActionResult> ExportAllOrders()
        {
            string rootFolder = _env.WebRootPath;
            string fileName = @"ExportAllOrders.csv";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(rootFolder, fileName));
            }

            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(file))
            {
                
                IList<Order> traineeList = _dbContext.Orders.ToList();

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
        [Route("ExportAllHelp")]
        public async Task<IActionResult> ExportAllHelp()
        {
            string rootFolder = _env.WebRootPath;
            string fileName = @"ExportAllHelp.csv";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(rootFolder, fileName));
            }

            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(file))
            {

               

               var  dbOrders = await _dbContext.HelpRequests.ToListAsync();
                var traineeList = dbOrders;

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("User");
                using (var cells = worksheet.Cells[1, 1, 1, 11]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                int totalRows = traineeList.Count();

                worksheet.Cells[1, 1].Value = "Request ID ";
                worksheet.Cells[1, 2].Value = "Owner Name";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Phone";
                worksheet.Cells[1, 5].Value = "Subject";
                worksheet.Cells[1, 6].Value = "Date Created";
                worksheet.Cells[1, 7].Value = "Message ";
          

                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {
                    worksheet.Cells[row, 1].Value = traineeList[i].Id;
                    worksheet.Cells[row, 2].Value = traineeList[i].Name;
                    worksheet.Cells[row, 3].Value = traineeList[i].Email;
                    worksheet.Cells[row, 4].Value = traineeList[i].Phone;
                    worksheet.Cells[row, 5].Value = traineeList[i].Subject;
                    worksheet.Cells[row, 6].Value = traineeList[i].DateCreated;
                    worksheet.Cells[row, 7].Value = traineeList[i].Message.ToString();
                   

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
        [Route("ExportAllSubscription")]
        public async Task<IActionResult> ExportSubscription()
        {
            string rootFolder = _env.WebRootPath;
            string fileName = @"ExportAllSubscription.csv";
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
