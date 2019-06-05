using CSBGlobal.Configuration;
using CSBGlobal.Helpers;
using CSBGlobal.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CSBGlobal.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailService : IEmailSender
    {
        private IHostingEnvironment _env { get; set; }
        SmtpClient SmtpServer;
        string MailerResponse;

        EmailTemplateHelper EmailHelper;
        public EmailService(IHostingEnvironment env)
        {
            _env = env;

            SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
            SmtpServer = smtpClient;
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailerResponse = "";
            EmailHelper = new EmailTemplateHelper(_env);



        }
        //New User
        public string SendNewUserConfirmation(ApplicationUser customer, string link)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(customer.Email);
                mail.Subject = "Welcome Pack";
                mail.IsBodyHtml = true;


                string setup = "https://smeproductivity.com/docs/termsandconditions", faq = "https://smeproductivity.com/docs/faqs", techsupport = "https://smeproductivity.com/docs/help";


                var body = EmailHelper.GetTemplate("NewCustomer")
                    .Replace("#CustomerName", customer.FirstName).Replace("#CustomerEmail", customer.Email).Replace("#url", link).Replace("#setupguide", setup).Replace("#faq", faq).Replace("#Help", techsupport);


                mail.Body = body;

                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch
            {
                MailerResponse = "Failure";
            }
            return MailerResponse;
        }









        public string SendSubscriptionExpirationMail(Order order)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(order.Email);
                mail.Subject = "Subscription Expiration Notice";
                mail.IsBodyHtml = true;


                string Subname = "";
                if (order.OrderDetails != null && order.OrderDetails.Count > 1)
                {
                    foreach (var item in order.OrderDetails)
                    {
                        Subname = Subname + " , " + item.Product.Name;
                    }
                }

                else if (order.OrderDetails != null && order.OrderDetails.Count == 1)
                {
                    Subname = order.OrderDetails.FirstOrDefault().Product.Name;
                }


                string ExpirationDate = order.NextPaymentDate.ToLongDateString();



                var body = EmailHelper.GetTemplate("SubscriptionExpirationNotice")
                    .Replace("#SubName", Subname).Replace("#ExpDate", ExpirationDate);


                mail.Body = body;

                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch
            {
                MailerResponse = "Failure";
            }
            return MailerResponse;
        }


        public string SendSubscriptionChargeFailed(Order order)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(order.Email);
                mail.Subject = "Subscription renewal charge Failed";
                mail.IsBodyHtml = true;


                string Subname = "";
                if (order.OrderDetails != null && order.OrderDetails.Count > 1)
                {
                    foreach (var item in order.OrderDetails)
                    {
                        Subname = Subname + " , " + item.Product.Name;
                    }
                }

                else if (order.OrderDetails != null && order.OrderDetails.Count == 1)
                {
                    Subname = order.OrderDetails.FirstOrDefault().Product.Name;
                }


                var ExpirationDate = order.NextPaymentDate;



                var body = EmailHelper.GetTemplate("SubscriptionExpirationNotice").Replace("#SubName", Subname).Replace("#ExpDate", ExpirationDate.ToLongDateString());


                mail.Body = body;

                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch
            {
                MailerResponse = "Failure";
            }
            return MailerResponse;
        }

        public string SendSubscriptionRenewalNotice(Order order, string days)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(order.Email);
                mail.Subject = "Subscription Renewal Notice";
                mail.IsBodyHtml = true;


                string Subname = "";
                if (order.OrderDetails != null && order.OrderDetails.Count > 1)
                {
                    foreach (var item in order.OrderDetails)
                    {
                        Subname = Subname + " , " + item.Product.Name;
                    }
                }

                else if (order.OrderDetails != null && order.OrderDetails.Count == 1)
                {
                    Subname = order.OrderDetails.FirstOrDefault().Product.Name;
                }


                var ExpirationDate = order.NextPaymentDate;



                var body = EmailHelper.GetTemplate("SubscriptionRenewalNotice").Replace("#SubName", Subname).Replace("#ExpDate", ExpirationDate.ToLongDateString()).Replace("#days", days);


                mail.Body = body;

                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch
            {
                MailerResponse = "Failure";
            }
            return MailerResponse;
        }











        public string SendSupportTicket(ApplicationUser customer, Support ticket)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(customer.Email);
                mail.CC.Add("SPOC@wragbysolutions.com");
                mail.Subject = "Support Ticket";
                mail.IsBodyHtml = true;

                string name = customer.FirstName + " " + customer.LastName;
                var body = EmailHelper.GetTemplate("SupportNotice")
        .Replace("#SupOwner", name).Replace("#Subject", ticket.Subject).Replace("#Department", ticket.Department).Replace("#DateCreated", ticket.DateCreated.ToString());


                mail.Body = body;

                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch
            {
                MailerResponse = "Failure";
            }
            return MailerResponse;
        }



        // New Customer Acc
        public string SendNewCustomerCreated(ApplicationUser customer)
        {

            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(customer.Email);
                mail.Subject = "Microsoft Customer Account Created";
                mail.IsBodyHtml = true;


                var username = customer.CspDefaultUserName + "@" + customer.Domain;
                var body = EmailHelper.GetTemplate("NewCSPCustomer")
                    .Replace("#CompanyName", customer.CompanyName).Replace("#TenantId", customer.CspId).Replace("#Domain", customer.Domain).Replace("#userlogin", username).Replace("#Password", customer.CspDefaultPassword);


                mail.Body = body;

                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception)
            {
                MailerResponse = "Failure";
            }
            return MailerResponse;

        }




        public string SendOrderCompletedMail(ApplicationUser customer, Order order)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(customer.Email);
                mail.Subject = "Welcome Pack ";
                mail.IsBodyHtml = true;


                var body = EmailHelper.GetTemplate("OrderSummary")
                    .Replace("#CompanyName", customer.CompanyName).Replace("#CompanyAddress", customer.CompanyAddress).Replace("#State", customer.State).Replace("#Country", customer.Country).Replace("#Phone", customer.PhoneNumber).Replace("#OrderLink", $"https://www.smeproductivity.com/CustomerDashboard/OrderDetails/{order.OrderId}").Replace("#Total", order.Total.ToString()).Replace("#URL", "https://partner.microsoft.com").Replace("#supportmail", Config.SupportMail);



                mail.Body = body;
                //todo: add remaing stuff

                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception)
            {
                MailerResponse = "Failure";
            }
            return MailerResponse;
        }
        //Forgot Password
        //public async Task<string> SendPasswordReset(string PasswordResetLink, string Email)
        public string SendPasswordReset(string PasswordResetLink, string Email)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(Email);
                mail.Subject = "FBN Technology Support Password Reset";
                mail.IsBodyHtml = true;

                mail.Body = EmailHelper.GetTemplate("ForgotPassword")
                    .Replace("#CustomerEmail", Email)
                    .Replace("#ResetLink", PasswordResetLink);
                //todo: add remaing stuff


                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception ex)
            {
                MailerResponse = "Failure";
                var ErrorMessage = ex.Message;
            }
            return MailerResponse;
        }


        //Purchase Confirmation
        //public async Task<string> SendPurchaseConfirmation(Order order, Customer customer)
        public string SendPurchaseConfirmation(Order order, Customer customer)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            MailMessage mailMessage = new MailMessage();
            try
            {
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(customer.Email);
                mail.Subject = "Your order is complete";
                mail.IsBodyHtml = true;

                mail.Body = EmailHelper.GetTemplate("OrderComplete")
                    .Replace("#CustomerName", customer.FirstName)
                    .Replace("#CustomerEmail", customer.Email)
                    .Replace("#subscription", order.Domain)
                    .Replace("#PaymentRef", order.OrderId.ToString())
                    .Replace("#PaymentAmount", order.Total.ToString());
                //todo: add remaing stuff

                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception)
            {
                MailerResponse = "Failure";
            }
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add("salesexecutives@wragbysolutions.com");
                mail.Subject = "New Order on the CSP Portal";
                mail.IsBodyHtml = true;

                mail.Body = EmailHelper.GetTemplate("OrderCompleteToWragby")
                    .Replace("#CustomerName", customer.FirstName)
                    .Replace("#AdminDash", "https://www.smeproductivity.com/administratordashboard");
                SmtpServer.Send(mail);
            }
            catch
            {

            }
            return MailerResponse;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }

        public string SendInvoice(InvoiceCustomerModel invoiceCustomerModel, int OrderID, string AdminLink)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            MailMessage mailMessage = new MailMessage();
            try
            {
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(invoiceCustomerModel.Email);
                mail.Subject = "Invoice Confirmation";
                mail.IsBodyHtml = true;

                mail.Body = EmailHelper.GetTemplate("Invoice")
                    .Replace("#CustomerName", invoiceCustomerModel.FirstName)
                     .Replace("#CustomerCompanyName", invoiceCustomerModel.CompanyName)
                     .Replace("#CustomerCompanyAddress", invoiceCustomerModel.CompanyAddress)
                     .Replace("#Phone", invoiceCustomerModel.Phone)
                     .Replace("#Email", invoiceCustomerModel.Email)
                      .Replace("#Phone", invoiceCustomerModel.Phone)
                     .Replace("#Email", invoiceCustomerModel.Email)
                    .Replace("#OrderLink", $"https://www.smeproductivity.com/AdministratorDashboard/OrderDetails/{OrderID}");
                //todo: add remaing stuff

                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception)
            {
                MailerResponse = "Failure";
            }
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                if (mail.To.Contains(new MailAddress(invoiceCustomerModel.Email)))
                    mail.To.Remove(new MailAddress(invoiceCustomerModel.Email));
                mail.To.Add("salesexecutives@wragbysolutions.com");
                mail.Subject = "New Invoice";
                mail.IsBodyHtml = true;

                mail.Body = EmailHelper.GetTemplate("InvoiceToWragby")
                    .Replace("#CustomerName", invoiceCustomerModel.FirstName)
                    .Replace("#AdminDash", AdminLink);
                SmtpServer.Send(mail);
            }
            catch
            {

            }
            return MailerResponse;
        }

        public string SendLinkEmailAsync(string emailAdd, string subject, string message)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(emailAdd);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception ex)
            {
                MailerResponse = "Failure";
                var ErrorMessage = ex.Message;
            }
            return MailerResponse;
        }

        public string SendPlainEmailAsync(string emailAdd, string subject, string message)
        {
            SmtpServer.Credentials = new System.Net.NetworkCredential("SPOC@wragbysolutions.com", "@$Wbst@m!n18");
            try
            {
                MailMessage mailMessage = new MailMessage();
                MailMessage mail = mailMessage;
                mail.From = new MailAddress("SPOC@wragbysolutions.com");
                mail.To.Add(emailAdd);
                mail.Subject = subject;
                mail.Body = message;
                SmtpServer.Send(mail);
                MailerResponse = "Success";
            }
            catch (Exception ex)
            {
                MailerResponse = "Failure";
                var ErrorMessage = ex.Message;
            }
            return MailerResponse;
        }
    }
}