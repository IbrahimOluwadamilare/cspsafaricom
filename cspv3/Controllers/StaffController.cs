using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cspv3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cspv3.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Support()
        {
            return View();
        }
       
        // GET: Staff/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult MyProfile()
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

    public ActionResult ManageOrder()
    {
      return View();
    }
       
        public async Task<IActionResult> ProductPortfolio()
    {
            
      return View(await _context.Products.ToListAsync());
    }

        public ActionResult CreateSupportTicket()
        {
            return View();
        }
        public ActionResult RevenueReport()
        {
            return View();
        }
        public ActionResult CustomerReport()
        {
            return View();
        }
        public ActionResult SettlementsReport()
        {
            return View();
        }
        public ActionResult SalesReport()
        {
            return View();
        }
        public async Task<IActionResult> OrdersReport()
        {
            
            return View(await _context.Orders.ToListAsync());
        }

        public ActionResult RenewalsReport()
        {
            return View();
        }
        // POST: Staff/Create
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

        // GET: Staff/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Staff/Edit/5
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

        // GET: Staff/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Staff/Delete/5
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