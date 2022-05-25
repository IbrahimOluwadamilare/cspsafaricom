using cspv3.Data;
using cspv3.Models;
using cspv3.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cspv3.Helpers
{
    public class BackgroundSubChecker
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAzureApi _azureservice;
        private readonly IEmailSender _emailsender;
        private readonly UserManager<ApplicationUser> _userManager;


        public BackgroundSubChecker(ApplicationDbContext context, IAzureApi azureApi, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _dbContext = context;
            _azureservice = azureApi;
            _userManager = userManager;
            _emailsender = emailSender;

        }


        public async Task<string> BackgroundSubTask()
        {
            var orders = await _dbContext.Orders.Include(o => o.OrderDetails).ThenInclude(p => p.Product).Where(a => a.FulfillPayment == true).ToListAsync();

            foreach (var order in orders)
            {
                if (order.NextPaymentDate.Subtract(DateTime.Now) >= TimeSpan.FromDays(2) && order.NextPaymentDate.Subtract(DateTime.Now) <= TimeSpan.FromDays(5))
                {

                    var Span = order.NextPaymentDate.Subtract(DateTime.Now);



                    // send notif
                    var sentcode = await _emailsender.SendSubscriptionRenewalNotice(order, Span.Days.ToString());

                    var mail = new StringBuilder();
                    mail.AppendLine("Sub Renewal notice Sent:" + sentcode);
                    mail.AppendLine("to: " + order.Email);
                    mail.AppendLine("for CSP order id: " + order.CspOrderId);
                    mail.AppendLine("for order id: " + order.OrderId);

                    return mail.ToString();

                }
                if (order.NextPaymentDate.Subtract(DateTime.Now) > TimeSpan.FromDays(5) && order.NextPaymentDate.Subtract(DateTime.Now) <= TimeSpan.FromDays(8))
                {
                    // send notif
                    var Span = order.NextPaymentDate.Subtract(DateTime.Now);

                    var sentcode = await _emailsender.SendSubscriptionRenewalNotice(order, Span.Days.ToString());
                    var mail = new StringBuilder();
                    mail.AppendLine("Sub Renewal notice Sent:" + sentcode);
                    mail.AppendLine("to: " + order.Email);
                    mail.AppendLine("for CSP order id: " + order.CspOrderId);
                    mail.AppendLine("for order id: " + order.OrderId);

                    return mail.ToString();
                }

                if (order.NextPaymentDate.Subtract(DateTime.Now) <= TimeSpan.FromDays(0) && order.NextPaymentDate.Subtract(DateTime.Now) >= TimeSpan.FromDays(-1))
                {
                    // suspend sub




                    var customer = await _userManager.FindByEmailAsync(order.Email);


                    var CspOrderId = order.CspOrderId;



                    if (CspOrderId != null)
                    {
                        var subscription = await _azureservice.GetSubscriptionbyOrderAsync(customer.CspId, CspOrderId);
                        if (subscription != null)
                        {
                            foreach (var sub in subscription.Items)
                            {

                                var suspendsub = await _azureservice.SuspendSubscriptionAsync(customer.CspId, sub.Id);

                                if (suspendsub != null)
                                {
                                    Console.WriteLine(suspendsub.Status);
                                }
                            }
                        }
                    }
                    var Span = order.NextPaymentDate.Subtract(DateTime.Now);
                    var sentcode = await _emailsender.SendSubscriptionExpirationMail(order);
                    var mail = new StringBuilder();
                    mail.AppendLine("Sub Suspended Notice Sent:" + sentcode);
                    mail.AppendLine("to: " + order.Email);
                    mail.AppendLine("for CSP order id: " + order.CspOrderId);
                    mail.AppendLine("for order id: " + order.OrderId);

                    return mail.ToString();
                }





            }

            return "No Sub to Track";

        }
    }
}
