using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSBGlobal.Data;
using CSBGlobal.Models;
using CSBGlobal.Models.Products;

namespace CSBGlobal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    // [Authorize]
    public class ProductController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        [HttpGet]
        public ActionResult<GenericResponse<List<ProductOffering>>> GetAllProducts()
        {
            try
            {

                var products = _context.Products.ToList();

                return Ok(new GenericResponse<List<ProductOffering>>
                {
                    Data = products,
                    Message = null,
                    Success = true

                });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new GenericResponse<List<ProductOffering>>
                {
                    Message = e.Message,
                    Data = null,
                    Success = false
                });
            }

        }

       

    }
}


