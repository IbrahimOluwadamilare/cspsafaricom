using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CSBGlobal.Models;
using CSBGlobal.Models.AccountViewModels;
using static CSBGlobal.Services.CustomerService;

namespace CSBGlobal.Services
{
    public interface ICustomerAuth
    {

        Task<GenericResponse<CustomerDetail>> CreateCustomerAsync(RegisterViewModel customer);
        Task<GenericResponse<Customer>> GetCustomerbyIdAsync(string customerId);

        Task<GenericResponse<string>> UpdateCustomerAsync(string customerId, Customer model);
        Task<GenericResponse<List<Order>>> GetTransactionHistoryAsync(string customerId);
    }
}
