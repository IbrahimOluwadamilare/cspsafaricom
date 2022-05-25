using cspv3.Configuration;
using cspv3.Data;
using cspv3.Helpers;
using cspv3.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace cspv3.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class SendGridEmailService : IEmailSender
    {
        private IHostingEnvironment _env { get; set; }
        private SendGridSettings AppSettings { get; set; }

        SendGridClient SmtpClient;
        string MailerResponse;
        List<EmailAddress> BccAddresses = new List<EmailAddress>
        {
            new EmailAddress
            {
                  Email = "vanugwa@wragbysolutions.com",

            },
            new EmailAddress
            {
                  Email = "tagabielesin@wragbysolutions.com",

            }
        };
        EmailTemplateHelper EmailHelper;
        public SendGridEmailService(IHostingEnvironment env, IOptions<SendGridSettings> settings)
        {
            _env = env;
            AppSettings = settings.Value;

            SmtpClient = new SendGridClient(AppSettings.SendGridKey);
          
            MailerResponse = "";
            EmailHelper = new EmailTemplateHelper(_env);



        }
        //New User
        public async Task<string> SendNewUserConfirmation(ApplicationUser customer, string link)
        {
           
            try
            {

                string setup = "https://csp.wragbysolutions.com/docs/termsandconditions", faq = "https://csp.wragbysolutions.com/docs/faqs", techsupport = "https://csp.wragbysolutions.com/docs/help";

                var body = EmailHelper.GetTemplate("NewCustomer")
.Replace("#CustomerName", customer.FirstName).Replace("#CustomerEmail", customer.Email).Replace("#url", link).Replace("#setupguide", setup).Replace("#faq", faq).Replace("#Help", techsupport);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),
                    
                    Subject = "Welcome Pack",
                    HtmlContent = body,
                    
                    

                };
                msg.AddTo(new EmailAddress(customer.Email));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

                MailerResponse = "Success";

                

            }
            catch (Exception e)
            {
                MailerResponse = "Failure  " + e.Message;
            }
            return MailerResponse;
        }



        public async Task<string> SendSubscriptionExpirationMail(Order order)
        {
           
            try
            {
                

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


                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = "Subscription Expiration Notice",
                    HtmlContent = body,



                };
                msg.AddTo(new EmailAddress(order.Email));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

                MailerResponse = "Success";
            }
            catch (Exception e)
            {
                MailerResponse = "Failure  " + e.Message;
            }
            return MailerResponse;
        }


        public async Task<string> SendSubscriptionChargeFailed(Order order)
        {
          
            try
            {
              
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


                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = "Subscription Renewal Charge Failed",
                    HtmlContent = body,



                };
                msg.AddTo(new EmailAddress(order.Email));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

                MailerResponse = "Success";
            }
            catch (Exception e)
            {
                MailerResponse = "Failure  " + e.Message;
            }
            return MailerResponse;
        }

        public async Task<string> SendSubscriptionRenewalNotice(Order order, string days)
        {
          
            try
            {
               

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


                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = "Subscription Renewal Notice",
                    HtmlContent = body,



                };
                msg.AddTo(new EmailAddress(order.Email));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

                MailerResponse = "Success";
            }
            catch (Exception e)
            {
                MailerResponse = "Failure  " + e.Message;
            }
            return MailerResponse;
        }











        public async Task<string> SendSupportTicket(ApplicationUser customer, Support ticket)
        {
           
            try
            {
               
                string name = customer.FirstName + " " + customer.LastName;
                var body = EmailHelper.GetTemplate("SupportNotice")
        .Replace("#SupOwner", name).Replace("#Subject", ticket.Subject).Replace("#Department", ticket.Department).Replace("#DateCreated", ticket.DateCreated.ToString());


                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = "Subscription Expiration Notice",
                    HtmlContent = body,
                    


                };
                msg.AddTo(new EmailAddress(customer.Email));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

                MailerResponse = "Success";
            }
            catch (Exception e)
            {
                MailerResponse = "Failure";
            }
            return MailerResponse;
        }



        // New Customer Acc
        public async Task<string> SendNewCustomerCreated(ApplicationUser customer)
        {

           
            try
            {
              

                var username = customer.CspDefaultUserName + "@" + customer.Domain;
                var body = EmailHelper.GetTemplate("NewCSPCustomer")
.Replace("#CompanyName", customer.CompanyName).Replace("#TenantId", customer.CspId).Replace("#Domain", customer.Domain).Replace("#userlogin", username).Replace("#Password", customer.CspDefaultPassword);


                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = "Microsoft Customer Account Created",
                    HtmlContent = body,



                };
                msg.AddTo(new EmailAddress(customer.Email));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

                MailerResponse = "Success";
            }
            catch (Exception)
            {
                MailerResponse = "Failure";
            }
            return MailerResponse;

        }




        public async Task<string> SendOrderCompletedMail(ApplicationUser customer, Order order)
        {
          
            try
            {
              

                var body = EmailHelper.GetTemplate("OrderSummary")
.Replace("#CompanyName", customer.CompanyName).Replace("#CompanyAddress", customer.CompanyAddress).Replace("#State", customer.State).Replace("#Country", customer.Country).Replace("#Phone", customer.PhoneNumber).Replace("#OrderLink", $"https://csp.wragbysolutions.com/CustomerDashboard/OrderDetails/{order.OrderId}").Replace("#Total", order.Total.ToString()).Replace("#URL", "https://partner.microsoft.com").Replace("#supportmail", Config.SupportMail);



                //todo: add remaing stuff

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = "Order Completed",
                    HtmlContent = body,



                };
                msg.AddTo(new EmailAddress(order.Email));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

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
        public async Task<string> SendPasswordReset(string PasswordResetLink, string Email)
        {
          
            try
            {
               

                var body = EmailHelper.GetTemplate("ForgotPassword")
                    .Replace("#CustomerEmail", Email)
                    .Replace("#ResetLink", PasswordResetLink);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = "Password Reset",
                    HtmlContent = body,



                };
                msg.AddTo(new EmailAddress(Email));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

                MailerResponse = "Success";
            }
            catch (Exception ex)
            {
                MailerResponse = "Failure";
                var ErrorMessage = ex.Message;
            }
            return MailerResponse;
        }


       
        public async Task<string> SendPurchaseConfirmation(Order order, Customer customer)
        {
          
            try
            {
              
                var body  = EmailHelper.GetTemplate("OrderComplete")
                    .Replace("#CustomerName", customer.FirstName)
                    .Replace("#CustomerEmail", customer.Email)
                    .Replace("#subscription", order.Domain)
                    .Replace("#PaymentRef", order.OrderId.ToString())
                    .Replace("#PaymentAmount", order.Total.ToString());


                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = "Order payment Successful",
                    HtmlContent = body,



                };
                msg.AddTo(new EmailAddress(order.Email));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

                MailerResponse = "Success";
            }
            catch (Exception)
            {
                MailerResponse = "Failure";
            }
          
           
            return MailerResponse;
        }


        public async Task<string> SendInvoice(InvoiceCustomerModel invoiceCustomerModel, int OrderID, string AdminLink)
        {

            try
            {

                var body = EmailHelper.GetTemplate("Invoice")
                     .Replace("#CustomerName", invoiceCustomerModel.FirstName)
                      .Replace("#CustomerCompanyName", invoiceCustomerModel.CompanyName)
                      .Replace("#CustomerCompanyAddress", invoiceCustomerModel.CompanyAddress)
                      .Replace("#Phone", invoiceCustomerModel.Phone)
                      .Replace("#Email", invoiceCustomerModel.Email)
                       .Replace("#Phone", invoiceCustomerModel.Phone)
                      .Replace("#Email", invoiceCustomerModel.Email)
                     .Replace("#OrderLink", $"https://csp.wragbysolutions.com/AdministratorDashboard/OrderDetails/{OrderID}");

                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = "Invoice Confirmation",
                    HtmlContent = body,



                };
                msg.AddTo(new EmailAddress(invoiceCustomerModel.Email));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

                MailerResponse = "Success";
            }
            catch (Exception)
            {
                MailerResponse = "Failure";
            }
           
            return MailerResponse;
        }

        public async Task<string> SendLinkEmailAsync(string emailAdd, string subject, string message)
        {
           
            try
            {
              


                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = subject,
                    HtmlContent = message,



                };
                msg.AddTo(new EmailAddress(emailAdd));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

                MailerResponse = "Success";


            }
            catch (Exception ex)
            {
                MailerResponse = "Failure";
                var ErrorMessage = ex.Message;
            }
            return MailerResponse;
        }

        public async Task<string> SendPlainEmailAsync(string emailAdd, string subject, string message)
        {
            try
            {



                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("SPOC@wragbysolutions.com", "Wragby CSP Platform"),

                    Subject = subject,
                    PlainTextContent = message,



                };
                msg.AddTo(new EmailAddress(emailAdd));
                msg.AddBccs(BccAddresses);

                await SmtpClient.SendEmailAsync(msg);

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