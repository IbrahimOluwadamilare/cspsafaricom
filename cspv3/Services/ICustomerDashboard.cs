using System.Collections.Generic;
using System.Threading.Tasks;
using cspv3.Models;
using cspv3.ViewModels;

namespace cspv3.Services
{
    public interface ICustomerDashboard
    {
        Task CustomerProfile();

        Task<IEnumerable<Support>> GetSupportsAsync(string CaseOwner);
        Task<IEnumerable<Order>> GetOrdersAsync(string userMail);
        Task<IEnumerable<Order>> GetFulfilledOrdersAsync(string userMail);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync(int id);
          Task<Order> GetOrderbyId(int orderid);

        Task GetActiveProductsAsync();

        Task GetCustomerInvoiceAsync();



    }
}
