using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CSBGlobal.Data;
using CSBGlobal.Models;
using CSBGlobal.Models.AccountViewModels;
using CSBGlobal.Models.Products;
using CSBGlobal.Services;
using static CSBGlobal.Services.CustomerService;

namespace CSBGlobal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    // [Authorize]

    public class CustomerController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        private readonly ICustomerAuth _customerservice;

        public CustomerController(ApplicationDbContext context, ICustomerAuth customerAuth)
        {
            _context = context;
            _customerservice = customerAuth;
        }
        [HttpPost]
        public async Task<ActionResult<GenericResponse<CustomerDetail>>> CreateCustomer([FromBody] RegisterViewModel customer)
        {
            var createdcustomer = await _customerservice.CreateCustomerAsync(customer);
            if (createdcustomer.Data == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, createdcustomer);

            }
            return Ok(createdcustomer);
        }

         [HttpPost]
        public async Task<ActionResult<GenericResponse<string>>> UpdateCustomer([FromBody] Customer customer, string CustomerId)
        {
            var updatedcustomer = await _customerservice.UpdateCustomerAsync(CustomerId, customer);

            if (updatedcustomer.Data == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, updatedcustomer);

            }
            return Ok(updatedcustomer);
        }

         [HttpGet]
        public async Task<ActionResult<GenericResponse<Customer>>> GetCustomerbyId( string customerId)
        {
            var createdcustomer = await _customerservice.GetCustomerbyIdAsync(customerId);

            if (!createdcustomer.Success)
            {
                return NotFound(createdcustomer);
            }

            return Ok(createdcustomer);
        }

        [HttpGet]
        public async Task<ActionResult<GenericResponse<List<Order>>>> GetTransactionHistory(string customerId)
        {

            var orders = await _customerservice.GetTransactionHistoryAsync(customerId);

            if (!orders.Success)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, orders);
            }

            return Ok(orders);
        }

    }
}
