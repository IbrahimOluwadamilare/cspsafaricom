using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CSBGlobal.Data;
using CSBGlobal.Models;
using CSBGlobal.Models.Products;
using CSBGlobal.Services;

namespace CSBGlobal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    // [Authorize]

    public class MarketPlaceController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        private readonly IMarketPlace _marketservice;

        public MarketPlaceController(ApplicationDbContext context, IMarketPlace marketPlace)
        {
            _context = context;
            _marketservice = marketPlace;
        }
        [HttpPost]
        public async Task<ActionResult<GenericResponse<Order>>> PlaceOrder([FromBody] Order order, string customerId)
        {
            var createorder = await _marketservice.CreateOrderAsync(order, customerId);

            return createorder;
        }

    }
}
