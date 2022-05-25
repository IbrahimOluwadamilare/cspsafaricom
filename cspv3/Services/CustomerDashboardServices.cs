using cspv3.Data;
using cspv3.Models;
using cspv3.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Services
{
    public class CustomerDashboardServices : ICustomerDashboard
    {
        private ApplicationDbContext _dbContext;

        public CustomerDashboardServices(ApplicationDbContext Context)
        {
            _dbContext = Context;
        }
        public Task CustomerProfile()
        {
            throw new NotImplementedException();
        }

        public Task GetActiveProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetCustomerInvoiceAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetFulfilledOrdersAsync(string userMail)
        {
            var orders = await _dbContext.Orders.Where(id => id.Email == userMail && id.FulfillPayment == true).ToListAsync();

            return orders;
        }

        public async Task<Order> GetOrderbyId(int orderid)
        {
            var order = await _dbContext.Orders.Include(b => b.OrderDetails).ThenInclude(p => p.Product). FirstOrDefaultAsync(a => a.OrderId == orderid);

            return order;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync(int id)
        {
            var details = await _dbContext.OrderDetails.Where(c => c.OrderId == id).ToListAsync();
            return details;

        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(string mail)
        {
            var orders = await _dbContext.Orders.Where(id => id.Email == mail).ToListAsync();

            return orders;
        }



        public async Task<IEnumerable<Support>> GetSupportsAsync(string CaseOwner)
        {
            var ticket = await _dbContext.SupportTicket.Where(user => user.CaseOwner == CaseOwner).ToListAsync();
            return ticket;
        }
    }
}
