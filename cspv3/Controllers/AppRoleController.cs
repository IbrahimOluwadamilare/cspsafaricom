using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cspv3.Data;
using cspv3.Models;
using cspv3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace cspv3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppRoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        ApplicationDbContext _dbcontext;
        public AppRoleController(RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            _dbcontext = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allUserRoles = _dbcontext.UserRoles.ToList();

            List<ApplicationRoleListViewModel> model = new List<ApplicationRoleListViewModel>();
            model = roleManager.Roles.Select(r => new ApplicationRoleListViewModel
            {
                RoleName = r.Name,
                Id = r.Id,
                Description = r.Description,
                NumberOfUsers = allUserRoles.Count(ur => ur.RoleId == r.Id)
            }).ToList();
            return View(model);
        }
        [Route("AppRole/EditRole")]
        [HttpGet]
        public async Task<IActionResult> AddEditApplicationRole(string id)
        {
            ApplicationRoleViewModel model = new ApplicationRoleViewModel();
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.RoleName = applicationRole.Name;
                    model.Description = applicationRole.Description;
                }
            }
            return View("EditRole", model);
        }
        [Route("AppRole/EditRole")]
        [ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<IActionResult> AddEditApplicationRole(string id, ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !String.IsNullOrEmpty(id);
                ApplicationRole applicationRole = isExist ? await roleManager.FindByIdAsync(id) :
               new ApplicationRole
               {
                   CreatedDate = DateTime.UtcNow

               };
                applicationRole.Name = model.RoleName;
                applicationRole.Description = model.Description;
                applicationRole.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                IdentityResult roleRuslt = isExist ? await roleManager.UpdateAsync(applicationRole)
                                                    : await roleManager.CreateAsync(applicationRole);
                if (roleRuslt.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [Route("AppRole/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("AppRole/Create")]
        [ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRoleViewModel model)
        {
            ApplicationRole applicationRole =
              new ApplicationRole
              {
                  CreatedDate = DateTime.UtcNow

              };
            applicationRole.Name = model.RoleName;
            applicationRole.Description = model.Description;
            applicationRole.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            IdentityResult roleRuslt = await roleManager.CreateAsync(applicationRole);
            if (roleRuslt.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Content("Operation Failed");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    name = applicationRole.Name;
                }
            }
            return View("Delete", name);
        }

        [ValidateAntiForgeryToken]

        [HttpPost]
        public async Task<IActionResult> Delete(string id, IFormCollection form)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    IdentityResult roleRuslt = roleManager.DeleteAsync(applicationRole).Result;
                    if (roleRuslt.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
    }
}