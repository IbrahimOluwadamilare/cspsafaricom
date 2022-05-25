using cspv3.Data;
using cspv3.Models;
using cspv3.Models.CspApiModels;
using cspv3.Models.CspApiModels.CustomerResponseModel;
using cspv3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cspv3.Helpers
{
    public class OrderCompletedHelper
    {
        private IEmailSender _emailservice;
        private ApplicationDbContext _dbContext;

        public OrderCompletedHelper(IEmailSender emailSender, ApplicationDbContext applicationDb)
        {
            _emailservice = emailSender;
            _dbContext = applicationDb;
        }
        public async Task<string> SendCustomerCreatedMail(ApplicationUser applicationUser, CustomersResponseModel createCustomerCsp)
        {
            var username = createCustomerCsp.UserCredentials.UserName;
            var domain = createCustomerCsp.CompanyProfile.Domain;
            var userlogin = username + "@" + domain;

            string mailSubject = "Customer Account Created";
            StringBuilder mailBody = new StringBuilder();

            mailBody.AppendLine("Congratulations.Your Customer Account has successfully been created on CSP Platfrom");
            mailBody.AppendLine();
            mailBody.AppendLine("Please find below your details:");
            mailBody.AppendLine();
            mailBody.AppendLine("Company Name: " + createCustomerCsp.CompanyProfile.CompanyName);
            mailBody.AppendLine("Tenant Id: " + createCustomerCsp.CompanyProfile.TenantId);
            mailBody.AppendLine("Domain: " + domain);

            mailBody.AppendLine("User Name: " + userlogin);
            mailBody.AppendLine("Password: " + createCustomerCsp.UserCredentials.Password);
            mailBody.AppendLine();

            try
            {

                mailBody.AppendLine();
                mailBody.AppendLine("Your Order is been provisioned on the CSP Platform. Once provisioning is completed, your user details would be sent to the mail you registered with. Thanks for your patience. ");

                mailBody.AppendLine();
                mailBody.AppendLine("You can Change your default password and Login at https://login.microsoftonline.com with your login details");



            }
            catch
            {

            }
            string response = await _emailservice.SendPlainEmailAsync(applicationUser.Email, mailSubject, mailBody.ToString());
            return response;
        }
        public async Task<string> SendServiceProvisionedMail(ApplicationUser applicationUser)
        {
            StringBuilder mailmsg = new StringBuilder();

            mailmsg.AppendLine("Congratulations");
            mailmsg.AppendLine();
            mailmsg.AppendLine("Your Products has successfully been provisioned on Microsoft CSP Platform. Please Login into your account administer your provisioned products.");

            var msg = await _emailservice.SendPlainEmailAsync(applicationUser.Email, "Order Created", mailmsg.ToString());
            return msg;
        }

        public CustomersModel BuildCustomerModel(ApplicationUser CSPUser, Order orderitem)
        {

            //creating an object of the api class customermodel for creation into csp via api
            NewCompanyProfile newCompanyProfile = new NewCompanyProfile
            {
                DomainInput = orderitem.Domain
            };

            Address address = new Address
            {
                FirstName = orderitem.FirstName,
                LastName = orderitem.LastName,
                AddressLine1 = orderitem.CompanyAddress,
                City = CSPUser.City,
                State = CSPUser.State,
                Country = CSPUser.Country,
                PostalCode = CSPUser.PostCode,
                PhoneNumber = orderitem.Phone

            };

            CustomerBillingProfile customerBillingProfile = new CustomerBillingProfile
            {

                Email = orderitem.Email,
                CompanyName = CSPUser.CompanyName,
                Address = address
            };

            CustomersModel profileCreationObject = new CustomersModel
            {

                NewCompanyProfile = newCompanyProfile,
                CustomerBillingProfile = customerBillingProfile
            };
            return profileCreationObject;
        }

        public CspOrderModel BuildOrderModel(Order orderitem, string TenantId)
        {

            var lineItems = new List<LineItem>();
            List<string> FriendlyName = new List<string>();
            List<int> Quantity = new List<int>();
            List<string> OfferId = new List<string>();
            var LineNo = new List<int>();
            var orderdetail = _dbContext.OrderDetails.Where(item => item.OrderId == orderitem.OrderId).ToList();



            var id = 0;

            foreach (var item in orderdetail)
            {
                Quantity.Add(item.Quantity);
                var productdetails = _dbContext.Products.FirstOrDefault(d => d.cspID == item.ProductId);
                FriendlyName.Add(productdetails.Name);
                if (productdetails.category == "Wragby Bundle")
                {
                    OfferId.Add(productdetails.cspID.Remove(productdetails.cspID.Length - 2));
                }
                else
                {
                    OfferId.Add(productdetails.cspID);
                }
                LineNo.Add(id);
                id++;

            }



            for (int i = 0; i < orderdetail.Count(); i++)
            {
                lineItems.Add(new LineItem()
                {
                    FriendlyName = FriendlyName[i],
                    LineItemNumber = LineNo[i],
                    Quantity = Quantity[i],
                    OfferId = OfferId[i],
                });



            }


            CspOrderModel model = new CspOrderModel
            {
                BillingCycle = "Monthly",
                ReferenceCustomerId = TenantId,
                LineItems = lineItems
            };
            return model;
        }




        public CspOrderModel BuildOrderForVmModel(Order orderitem, string TenantId)
        {

            var lineItems = new List<LineItem>();
            List<string> FriendlyName = new List<string>();
            List<int> Quantity = new List<int>();
            List<string> OfferId = new List<string>();
            var LineNo = new List<int>();
            var orderdetail = _dbContext.OrderDetails.Where(item => item.OrderId == orderitem.OrderId).ToList();



            var id = 0;

            foreach (var item in orderdetail)
            {
                Quantity.Add(item.Quantity);
                var productdetails = _dbContext.SubProducts.FirstOrDefault(d => d.ResouceId == item.ProductId);
                FriendlyName.Add(productdetails.Name);


                OfferId.Add(productdetails.ResouceId);

                LineNo.Add(id);
                id++;

            }



            for (int i = 0; i < orderdetail.Count(); i++)
            {
                lineItems.Add(new LineItem()
                {
                    FriendlyName = FriendlyName[i],
                    LineItemNumber = LineNo[i],
                    Quantity = Quantity[i],
                    OfferId = OfferId[i],
                });



            }


            CspOrderModel model = new CspOrderModel
            {
                BillingCycle = "Monthly",
                ReferenceCustomerId = TenantId,
                LineItems = lineItems
            };
            return model;
        }
    }



}
