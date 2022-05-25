using System.Collections.Generic;
using System.Threading.Tasks;
using cspv3.Models;
using cspv3.ViewModels;

namespace cspv3.Services
{
    public interface IAdministratorDashboard
    {
   
        Task AdminControlCenter();
        Task<IEnumerable<ApplicationUser>> GetRegisteredUsersAsync();

        IEnumerable<OrderDetail> OrderDetails(int id);

        Task FulFillOrder(int id);

        Task FulFillInvoiceOrder(int id);

        Task AdminProfile();

        Task Promotions();

        Task<IEnumerable<Order>> GetOrdersAsync();

        Task ManageProducts();

        Task ManageCustomers();

        Task<IEnumerable<Customer>> GetAllCustomer();

        Task FulFillInvoiceUpload(FulFillInvoiceModel fulFillInvoiceModel);

        Task GetRandomString();

        Task Promotions(PromotionModel promotion);

        Task ProductUpload();

        Task ManageOrdersearch(ManageOrdersearchModel searchString);


    }
}
