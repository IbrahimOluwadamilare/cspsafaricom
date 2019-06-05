using CSBGlobal.Models;
using CSBGlobal.Models.CspApiModels;
using CSBGlobal.Models.CspApiModels.AddLicense;
using CSBGlobal.Models.CspApiModels.CustomerResponseModel;
using CSBGlobal.Models.CspApiModels.License;
using CSBGlobal.Models.CspApiModels.Licenses;
using CSBGlobal.Models.CspApiModels.Offers;
using CSBGlobal.Models.CspApiModels.Order;
using CSBGlobal.Models.CspApiModels.Target;
using CSBGlobal.Models.CspApiModels.UserResponse;
using CSBGlobal.Models.DomainModels;
using CSBGlobal.Services.CBilling;
using CSBGlobal.Services.CProfile;
using CSBGlobal.Services.CResponse;
using CSBGlobal.Services.OrderResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Services
{
    public interface ICSPapi
    {
        Task<GenericResponse<bool>> IsDomainAvailableAsync(string domain);
        //offers
        Task<AllOffersModel> GetAllOffersAsync();

        Task<OffersModel> GetMyOffersAsync();



        //customer
        Task<GenericResponse<CustomersResponseModel>> CreateCustomerAsync(CustomersModel item);
        Task<object> CreateCustomerForIndirectSellerAsync(CustomersModel item);
        Task<UserModel> CreateuserforcustomerAsync(UserModel model, string customerId);
        Task<UsersResponse> GetcustomerUsersAsync(string customerId);
        Task<Models.CspApiModels.UserResponse.Item> GetcustomerUserbyIdAsync(string selectedCustomerId, string customerUserId);

        Task<SubscribedSku> GetSubscribedSkuAsync(string SelectedCustomerId);


        Task<string> DeleteUserforCustomerAsync(string selectedCustomerId, string customerUserIdToDelete);

        Task<CustomerResponse> GetCustomerbyId(string CustomerId);
        Task<CustomerBillingResponse> GetCustomerBillingProfile(string CustomerId);
        Task<CompanyProfileResponse> GetCustomercompanyprofile(string CustomerId);
        Task<object> CheckInventory(string CustomerId, string SubscriptionId, string CountryCode, string ProductId);
        Task<AddLicensesResponse> AssignLicense(string selectedProductSkuId, string selectedCustomerId, string selectedCustomerUserId);



        Task<LicensesResponse> GetLicensesForUser(string CustomerId, string CustomerUserId);
        Task<object> GetLicensesForUserLicenseGroup(string CustomerId, string CustomerUserId);
        Task<object> GetOfferAddOns(string OfferId);

        //subscription

        Task<object> ChangeQuantity(string CustomerId, string SubscriptionId);

        Task<object> TrialtoPaid(string CustomerId, string SubscriptionId);
        Task<object> GetAddonSubscriptions(string CustomerId, string SubscriptionId);
        Task<object> GetSubscriptionbyId(string CustomerId, string SubscriptionId);
        Task<object> GetSubscriptionProvisioningStatus(string CustomerId, string SubscriptionId);
        Task<object> GetSubscriptionRegStatus(string CustomerId, string SubscriptionId);

        Task<object> ReactivateSubscription();
        Task<object> RegisterSubscription(string CustomerId, string SubscriptionId);
        Task<object> Suspendsubscription(SubscriptionModel sub, string Subscription);

        Task<object> Transitionsubscription(TargetOfferModel model, string SubscriptionId, string customerId);

        //Order
        Task<CspOrderResponse> CreateOrderAsync(string customerId, CspOrderModel model);
        Task<object> CreateOrderForIndirectReseller(OrderModel model, string CustomerId, string ResellerId);
        Task<object> GetOrderbyId(string CustomerId, string OrderId);
        Task<object> PurchaseSubscriptionAddOn(OrderModel model, string CustomerId, string SubdcriptionId);




    }
}
