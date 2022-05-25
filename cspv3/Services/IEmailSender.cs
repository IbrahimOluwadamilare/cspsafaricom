using cspv3.Models;
using System.Threading.Tasks;

namespace cspv3.Services
{
    public interface IEmailSender
    {
        Task<string> SendLinkEmailAsync(string emailAdd, string subject, string message);
        Task<string> SendPlainEmailAsync(string emailAdd, string subject, string message);
        Task<string> SendNewUserConfirmation(ApplicationUser customer, string link);
        Task<string> SendNewCustomerCreated(ApplicationUser customer);
        Task<string> SendSupportTicket(ApplicationUser customer, Support ticket);
        Task<string> SendOrderCompletedMail(ApplicationUser customer, Order order);
        Task<string> SendSubscriptionExpirationMail(Order order);

        Task<string> SendSubscriptionChargeFailed(Order order);
        Task<string> SendSubscriptionRenewalNotice(Order order, string days);


    }
}
