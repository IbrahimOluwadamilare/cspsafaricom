using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cspv3.Data;
using cspv3.Models;
using cspv3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cspv3.Controllers
{
    public class PartnerController : Controller
    {
        private readonly ICustomerDashboard _customerDashboard;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public PartnerController(UserManager<ApplicationUser> userManager, ICustomerDashboard customerDashboard, ApplicationDbContext context)
        {
            _customerDashboard = customerDashboard;
            _dbContext = context;
            _userManager = userManager;
        }
        // GET: Partner
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Support()
        {
            return View();
        }
        // GET: Partner/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Partner/Create
        public ActionResult Create()
        {
            return View();
        }

    public ActionResult EditProfile()
    {
      return View();
    }


    public ActionResult ManageProduct()
    {
      return View();
    }

        public async Task<IActionResult> Orders(string sortOrder)
        {
            var user = await _userManager.GetUserAsync(User);
            var orderdata = await _customerDashboard.GetOrdersAsync(user.Email);
            switch (sortOrder)
            {
                case "Orderid_desc":
                    orderdata = orderdata.OrderByDescending(s => s.OrderId);
                    break;
                default:
                    orderdata = orderdata.OrderBy(s => s.OrderId);
                    break;
            }


            return View(orderdata.Where(a => a.FulfillPayment == true).ToList());
        }




        // POST: Partner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Partner/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Partner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Partner/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Partner/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}