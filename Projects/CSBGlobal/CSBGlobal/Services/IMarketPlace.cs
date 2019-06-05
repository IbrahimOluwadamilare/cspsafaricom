using CSBGlobal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Services
{
   public interface IMarketPlace
    {
        Task<GenericResponse<Order>> CreateOrderAsync(Order order, string CustomerId);
    }
}
