using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSBGlobal.Data;
using CSBGlobal.Helpers;
using CSBGlobal.Models;

namespace CSBGlobal.Services
{
    public class MarketService : IMarketPlace
    {
        private readonly IEmailSender _emailservice;
        private readonly ApplicationDbContext _context;
        private readonly ICSPapi _cspApi;

        public MarketService(IEmailSender emailSender, ApplicationDbContext context, ICSPapi CSPapi)
        {
            _emailservice = emailSender;
            _context = context;
            _cspApi = CSPapi;
        }
        public async Task<GenericResponse<Order>> CreateOrderAsync(Order order, string CustomerId)
        {
            OrderCompletedHelper Helper = new OrderCompletedHelper(_emailservice, _context);

            var Customer = _context.Users.SingleOrDefault(a => a.Id == CustomerId);
            if (Customer.CspId == null)
            {
                var model = Helper.BuildOrderModel(order, Customer.CspId);
                var CreateOrder = await _cspApi.CreateOrderAsync(Customer.CspId, model);
                if (CreateOrder != null)
                {
                    order.FulfillPayment = true;
                    order.FulFillmentDate = DateTime.Now;
                    order.LastPaymentDate = DateTime.Now;
                    order.NextPaymentDate = DateTime.Now.AddDays(30);
                    order.CspOrderId = CreateOrder.Id;

                    _context.Update(order);
                    _context.SaveChanges();



                    return new GenericResponse<Order>
                    {
                        Data = order,
                        Message = null,
                        Success = true

                    };
                        
                }

                return new GenericResponse<Order>
                {
                    Data = null,
                    Message = "unable to create order",
                    Success = false

                };

            }
            return new GenericResponse<Order>
            {
                Data = null,
                Message = "Customer does not exist",
                Success = false

            };
        }
    }
}
