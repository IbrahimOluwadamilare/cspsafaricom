using cspv3.Models.CspApiModels;
using cspv3.Models.CspApiModels.AddLicense;
using cspv3.Models.CspApiModels.CustomerResponseModel;
using cspv3.Models.CspApiModels.License;
using cspv3.Models.CspApiModels.Licenses;
using cspv3.Models.CspApiModels.Offers;
using cspv3.Models.CspApiModels.Order;
using cspv3.Models.CspApiModels.Target;
using cspv3.Models.CspApiModels.UserResponse;
using cspv3.Models.DomainModels;
using cspv3.Services.CBilling;
using cspv3.Services.CProfile;
using cspv3.Services.CResponse;
using cspv3.Services.OrderResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Services
{
    public interface ICSPapi
    {



        Task<bool> IsDomainAvailableAsync(string domain);
        //offers
        Task<AllOffersModel> GetAllOffersAsync();

        Task<OffersModel> GetMyOffersAsync();



        //customer
        Task<CustomersResponseModel> CreateCustomerAsync(CustomersModel item);
        Task<object> CreateCustomerForIndirectSellerAsync(CustomersModel item);
        Task<UserModel> CreateuserforcustomerAsync(UserModel model, string customerId);
        Task<UserModel> RestoreUserforcustomerAsync(string customerId, string customerUserId);
        Task<UsersResponse> GetcustomerUsersAsync(string customerId);
        Task<UsersResponse> GetDeletedcustomerUsersAsync(string customerId);
        Task<Models.CspApiModels.UserResponse.Item> GetcustomerUserbyIdAsync(string selectedCustomerId, string customerUserId);

         Task<SubscribedSku> GetSubscribedSkuAsync(string SelectedCustomerId);


        Task<string> DeleteUserforCustomerAsync(string selectedCustomerId, string customerUserIdToDelete);
        Task<CustomerEntitlements> GetcollectionofentitlementsAsync(string CustomerId);
        Task<CustomerResponse> GetCustomerbyId(string CustomerId);
        Task<CustomerBillingResponse> GetCustomerBillingProfile(string CustomerId);
        Task<CompanyProfileResponse> GetCustomercompanyprofile(string CustomerId);
        Task<object> CheckInventory(string CustomerId, string SubscriptionId, string CountryCode, string ProductId);
        Task<AddLicensesResponse> AssignLicense(string selectedProductSkuId, string selectedCustomerId, string selectedCustomerUserId);
        Task<CspDomainResponse> AddDomainForCustomerAsync(CspDomainRequest request, string customerId);


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
