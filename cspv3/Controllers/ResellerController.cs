using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cspv3.Controllers
{
    public class ResellerController : Controller
    {
        // GET: Reseller
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reseller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reseller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reseller/Create
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

        // GET: Reseller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reseller/Edit/5
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

        // GET: Reseller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reseller/Delete/5
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