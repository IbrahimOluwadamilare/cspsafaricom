using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cspv3.Data;
using cspv3.Models;
using cspv3.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace cspv3.Services
{
    public class AdministratorDashboardService : IAdministratorDashboard
    {
        private ApplicationDbContext _dbContext;

        public AdministratorDashboardService(ApplicationDbContext Context)
        {
            _dbContext = Context;
        }

        public Task AdminControlCenter()
        {
            throw new NotImplementedException();
        }

        public Task AdminProfile()
        {
            throw new NotImplementedException();
        }

        public Task FulFillInvoiceOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task FulFillInvoiceUpload(FulFillInvoiceModel fulFillInvoiceModel)
        {
            throw new NotImplementedException();
        }

        public Task FulFillOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllCustomer()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApplicationUser>> GetRegisteredUsersAsync()
        {
            var item = await _dbContext.Users.ToListAsync();
            return item;

        }

        public Task GetRandomString()
        {
            throw new NotImplementedException();
        }

        public Task ManageCustomers()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public Task ManageOrdersearch(ManageOrdersearchModel searchString)
        {
            throw new NotImplementedException();
        }

        public Task ManageProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetail> OrderDetails(int id)
        {
            return _dbContext.OrderDetails.Where(c => c.OrderId == id).ToList();
        }

        public Task Promotions()
        {
            throw new NotImplementedException();
        }

        public Task Promotions(PromotionModel promotion)
        {
            throw new NotImplementedException();
        }

        public Task ProductUpload()
        {
            throw new NotImplementedException();
        }

     

        //    private async Task<Customer> findCustomer(string id)
        //    {
        //        var customer  = await _dbContext.
        //    }
    }
}
