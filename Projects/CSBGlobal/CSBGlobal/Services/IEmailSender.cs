using CSBGlobal.Models;
using System.Threading.Tasks;

namespace CSBGlobal.Services
{
    public interface IEmailSender
    {
        string SendLinkEmailAsync(string emailAdd, string subject, string message);
        string SendPlainEmailAsync(string emailAdd, string subject, string message);
        string SendNewUserConfirmation(ApplicationUser customer, string link);
        string SendNewCustomerCreated(ApplicationUser customer);
        string SendSupportTicket(ApplicationUser customer, Support ticket);
        string SendOrderCompletedMail(ApplicationUser customer, Order order);
        string SendSubscriptionExpirationMail(Order order);
        string SendSubscriptionChargeFailed(Order order);
        string SendSubscriptionRenewalNotice(Order order, string days);

        Task SendEmailAsync(string email, string subject, string message);


    }
}
