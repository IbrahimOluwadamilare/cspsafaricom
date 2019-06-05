using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CSBGlobal.Data;
using CSBGlobal.Helpers;
using CSBGlobal.Models;
using CSBGlobal.Models.AccountViewModels;
using CSBGlobal.Models.CspApiModels;

namespace CSBGlobal.Services
{
    public class CustomerService : ICustomerAuth
    {
        private readonly IEmailSender _emailservice;
        private readonly ApplicationDbContext _context;
        private readonly ICSPapi _cspApi;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CustomerService(IEmailSender emailSender, ApplicationDbContext context, ICSPapi CSPapi, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _emailservice = emailSender;
            _context = context;
            _cspApi = CSPapi;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        public async Task<GenericResponse<string>> UpdateCustomerAsync(string customerId, Customer model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(customerId);
                if (user == null)
                {
                    return new GenericResponse<string>
                    {
                        Data = null,
                        Message = "Customer account not found",
                        Success = false

                    };
                }

                user.City = model.City;
                user.CompanyAddress = model.CompanyAddress;
                user.CompanyName = model.CompanyName;
                user.Country = model.Country;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.PostCode = model.PostCode;
                user.State = model.State;
                user.UserAddress = model.UserAddress;


                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new GenericResponse<string>
                    {
                        Data = "Customer Account Updated Successfully",
                        Message = null,
                        Success = true

                    };
                }

                else
                {
                    return new GenericResponse<string>
                    {
                        Data = null,
                        Message = "Unable to Update Customer Account",
                        Success = false

                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<string>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false

                };
            }
        }


        public async Task<GenericResponse<Customer>> GetCustomerbyIdAsync(string customerId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.Id == customerId);

            if (user == null)
            {
                return new GenericResponse<Customer>
                {
                    Data = null,
                    Message = "Customer account not found",
                    Success = false

                };
            }

            else
            {
                var customer = new Customer
                {
                    City = user.City,
                    CompanyAddress = user.CompanyAddress,
                    CompanyName = user.CompanyName,
                    Country = user.Country,
                    //  CspId = user.CspId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    RegistrationDate = user.RegisterationDate,
                    State = user.State,
                    UserAddress = user.UserAddress,
                    UserName = user.UserName,

                };
                return new GenericResponse<Customer>
                {
                    Data = customer,
                    Message = null,
                    Success = true

                };

            }

        }

        public async Task<GenericResponse<CustomerDetail>> CreateCustomerAsync(RegisterViewModel model)
        {
            try
            {
                var user = new ApplicationUser()
                {

                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    CompanyAddress = model.CompanyAddress,
                    CompanyName = model.CompanyName,
                    UserAddress = model.UserAddress,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CspId = model.CspId,
                    PostCode = model.PostCode,
                    State = model.State,
                    City = model.City,
                    Country = model.Country,
                    Level = 3,
                    RegisterationDate = DateTime.Now.Date,
                    //  Domain = model.Domain,



                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    var helper = new OrderCompletedHelper(_emailservice, _context);
                    var cspmodel = helper.BuildCustomerModel(user, model.Domain);

                    var cspCustomer = await _cspApi.CreateCustomerAsync(cspmodel);

                    if (cspCustomer.Data != null)
                    {

                        user.Domain = cspCustomer.Data.CompanyProfile.Domain;
                        user.CspId = cspCustomer.Data.CompanyProfile.TenantId;
                        user.BillingProfileId = cspCustomer.Data.BillingProfile.Id;
                        user.Etag = cspCustomer.Data.BillingProfile.Attributes.Etag;
                        user.CspDefaultUserName = cspCustomer.Data.UserCredentials.UserName;
                        user.CspDefaultPassword = cspCustomer.Data.UserCredentials.Password;

                        await _userManager.UpdateAsync(user);


                        return new GenericResponse<CustomerDetail>
                        {
                            Data = new CustomerDetail { CustomerId = user.Id, Csppassword = cspCustomer.Data.UserCredentials.Password, Cspusername = cspCustomer.Data.UserCredentials.UserName },
                            Message = null,
                            Success = true

                        };
                    }
                    else
                    {

                        return new GenericResponse<CustomerDetail>
                        {
                            Data = new CustomerDetail { CustomerId = user.Id, Csppassword = null, Cspusername = null },
                            Message = "Customer account created, " + cspCustomer.Message,
                            Success = true

                        };
                    }
                }
                else
                {
                    return new GenericResponse<CustomerDetail>
                    {
                        Data = null,
                        Message = result.Errors.FirstOrDefault().Description,
                        Success = false

                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<CustomerDetail>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false

                };

            }
        }


        public async Task<GenericResponse<List<Order>>> GetTransactionHistoryAsync(string customerId)
        {

            try
            {
                var user = await _userManager.FindByIdAsync(customerId);
                if (user == null)
                {
                    return new GenericResponse<List<Order>>
                    {
                        Data = null,
                        Message = "Customer account not found",
                        Success = false

                    };
                }
                var customerOrders = await _context.Orders.Include(a => a.OrderDetails).Where(b => b.Email == user.Email).ToListAsync();

                return new GenericResponse<List<Order>>
                {
                    Data = customerOrders,
                    Message = null,
                    Success = true

                };

            }
            catch (Exception e)
            {
                return new GenericResponse<List<Order>>
                {
                    Message = e.Message,
                    Data = null,
                    Success = false
                };
            }
        }
        public class CustomerDetail
        {
            public string CustomerId { get; set; }
            public string Cspusername { get; set; }
            public string Csppassword { get; set; }
            public string Role { get; set; }
        }
    }
}