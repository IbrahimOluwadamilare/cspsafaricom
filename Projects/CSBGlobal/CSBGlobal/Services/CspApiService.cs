using CSBGlobal.Models.CspApiModels;
using CSBGlobal.Models.CspApiModels.CustomerResponseModel;
using CSBGlobal.Models.CspApiModels.Order;
using CSBGlobal.Models.CspApiModels.Target;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using Newtonsoft.Json.Serialization;
using CSBGlobal.Services.CResponse;
using CSBGlobal.Services.CBilling;
using CSBGlobal.Services.CProfile;
using CSBGlobal.Services.OrderResponse;
using CSBGlobal.Models.CspApiModels.Offers;
using CSBGlobal.Models.CspApiModels.UserResponse;
using CSBGlobal.Models.CspApiModels.License;
using CSBGlobal.Models.CspApiModels.Licenses;
using CSBGlobal.Models.CspApiModels.AddLicense;
using CustUser = CSBGlobal.Models.CspApiModels.UserResponse.Item;
using CSBGlobal.Models.DomainModels;
using CSBGlobal.Configuration;
using CSBGlobal.Models;

namespace CSBGlobal.Services
{
    public class CspApiService : ICSPapi
    {
        public const string BaseUrlString = "https://csp-api-test.azurewebsites.net/api/v3/{0}";

        private HttpClient Client { get; }

        public CspApiService()
        {
            Client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(1020)
            };
        }


        public async Task<UsersResponse> GetcustomerUsersAsync(string customerId)
        {
            var add = "https://csp-api-test.azurewebsites.net/api/v3/customer/getcustomerusers?selectedCustomerId={0}";
            var address = new Uri(string.Format(add, customerId));
            try
            {
                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<UsersResponse>(strResult);

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }

        public async Task<CustUser> GetcustomerUserbyIdAsync(string selectedCustomerId, string customerUserId)
        {

            var address = string.Format(BaseUrlString, "customer/getcustomeruserbyid");
            var queryString = new Dictionary<string, string>()
            {
                { "selectedCustomerId", selectedCustomerId},

                { "customerUserId", customerUserId}
            };
            var requestUri = QueryHelpers.AddQueryString(address, queryString);

            try
            {
                using (var response = await Client.GetAsync(requestUri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<CustUser>(strResult);

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }


        public async Task<GenericResponse<CustomersResponseModel>> CreateCustomerAsync(CustomersModel item)
        {
            try
            {
                var address = new Uri(string.Format(BaseUrlString, "customer/createcustomer"));


                var json = JsonConvert.SerializeObject(item);
                var demo = JObject.Parse(json);
                var content = new StringContent(demo.ToString(), Encoding.UTF8, "application/json");


                using (var response = await Client.PostAsync(address, content))
                {
                    var strResult = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {



                        return new GenericResponse<CustomersResponseModel>
                        {
                            Data = JsonConvert.DeserializeObject<CustomersResponseModel>(strResult),
                            Message = null,
                            Success = true
                        };

                    }

                    else
                    {

                        return new GenericResponse<CustomersResponseModel>
                        {
                            Data = null,
                            Message = strResult,
                            Success = false
                        };
                    }
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<CspOrderResponse> CreateOrderAsync(string customerId, CspOrderModel model)
        {

            var address = string.Format(BaseUrlString, "order/createorder");
            try
            {
                var queryString = new Dictionary<string, string>()
                {
                    { "customerId", customerId}
                };

                var requestUri = QueryHelpers.AddQueryString(address, queryString);


                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);


                request.Headers.Add("Accept", "application/json");
                var json = JsonConvert.SerializeObject(model);

                request.Content = new StringContent(
                            json,
                            Encoding.UTF8,
                            "application/json"
                        );

                // Send the request
                using (var response = await Client.SendAsync(request))
                {
                    var strResult = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {


                        return JsonConvert.DeserializeObject<CspOrderResponse>(strResult);

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }

        public async Task<bool> IsDomainAvailable(string domainUrl)
        {
            var address = new Uri(string.Format(BaseUrlString, "utility/isdomainavailable"));

            var values = new Dictionary<string, string>
            {
                { "domainPrefix", domainUrl }
            };
            var content = new FormUrlEncodedContent(values);
            using (var client = new HttpClient())
            {
                try
                {
                    using (var response = await Client.PostAsync(address, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var strResult = await response.Content.ReadAsStringAsync();


                            return JsonConvert.DeserializeObject<bool>(strResult);

                        }
                        return false;
                    }
                }
                catch (OperationCanceledException)
                {
                    return false;
                }
            }
        }

        public async Task<SubscribedSku> GetSubscribedSkuAsync(string SelectedCustomerId)
        {
            var add = "https://csp-api-test.azurewebsites.net/api/v3/license/getsubscribedskus?selectedCustomerUserId={0}";
            var address = new Uri(string.Format(add, SelectedCustomerId));
            try
            {
                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<SubscribedSku>(strResult); ;

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public async Task<OffersModel> GetMyOffersAsync()
        {

            try
            {


                var address = new Uri(string.Format(BaseUrlString, "customer/myoffers"));


                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<OffersModel>(strResult); ;

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }



        public async Task<AllOffersModel> GetAllOffersAsync()
        {

            try
            {


                var address = new Uri(string.Format(BaseUrlString, "customer/getalloffers"));


                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<AllOffersModel>(strResult); ;

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public Task<object> CreateCustomerForIndirectSellerAsync(CustomersModel item)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponse<bool>> IsDomainAvailableAsync(string domain)
        {
            var add = "https://csp-api-test.azurewebsites.net/api/v3/utility/isdomainavailable?domainPrefix={0}";

            var requestUri = new Uri(string.Format(add, domain + Config.Domainsuffix));

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            using (var response = await Client.SendAsync(request))
            {
                var str = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return new GenericResponse<bool> { Data = bool.Parse(str), Message = null, Success = true };
                }
                else return new GenericResponse<bool> { Data = false, Message = str, Success = false }; ;

            }
        }


        public async Task<UserModel> CreateuserforcustomerAsync(UserModel item, string customerId)
        {
            try
            {
                var add = "https://csp-api-test.azurewebsites.net/api/v3/customer/createuserforcustomer?selectedCustomerId={0}";
                var address = new Uri(string.Format(add, customerId));


                var json = JsonConvert.SerializeObject(item);
                var demo = JObject.Parse(json);
                var content = new StringContent(demo.ToString(), Encoding.UTF8, "application/json");


                using (var response = await Client.PostAsync(address, content))
                {
                    var str = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<UserModel>(strResult);

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }
        public async Task<LicensesResponse> GetLicensesForUser(string CustomerId, string CustomerUserId)
        {
            var address = string.Format(BaseUrlString, "license/getlicensesasignedtouser");

            var queryString = new Dictionary<string, string>()
{
    { "selectedCustomerId", CustomerId},
     { "selectedCustomerUserId", CustomerUserId}
};
            var requestUri = QueryHelpers.AddQueryString(address, queryString);

            try
            {
                using (var response = await Client.GetAsync(requestUri))
                {
                    var strResult = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<LicensesResponse>(strResult);

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }

        public async Task<string> DeleteUserforCustomerAsync(string selectedCustomerId, string customerUserIdToDelete)
        {

            var address = string.Format(BaseUrlString, "customer/deleteuserforcustomer");

            var queryString = new Dictionary<string, string>()
{
    { "selectedCustomerId", selectedCustomerId},
     { "customerUserIdToDelete", customerUserIdToDelete}
};
            var requestUri = QueryHelpers.AddQueryString(address, queryString);

            try
            {
                using (var response = await Client.DeleteAsync(requestUri))
                {

                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return "deleted";

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }


        public async Task<CustomerResponse> GetCustomerbyId(string CustomerId)
        {
            var add = "https://csp-api-test.azurewebsites.net/api/v3/customer/getcustomerbyid?customerIdToRetrieve=";
            var address = new Uri(string.Format(add, CustomerId));
            try
            {
                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<CustomerResponse>(strResult); ;

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<CustomerBillingResponse> GetCustomerBillingProfile(string CustomerId)
        {
            var add = "https://csp-api-test.azurewebsites.net/api/v3/customer/getcustomerbillingprofile?selectedCustomerId={0}";
            var address = new Uri(string.Format(add, CustomerId));
            try
            {
                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<CustomerBillingResponse>(strResult); ;

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<CompanyProfileResponse> GetCustomercompanyprofile(string CustomerId)
        {
            var add = "https://csp-api-test.azurewebsites.net/api/v3/customer/getcustomercompanyprofile?selectedCustomerID={0}";
            var address = new Uri(string.Format(add, CustomerId));
            try
            {
                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<CompanyProfileResponse>(strResult); ;

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<CustomerOrderResponse> GetCustomerOrdersAsync(string CustomerId)
        {
            var add = "https://csp-api-test.azurewebsites.net/api/v3/customer/getallcustomerorders?selectedCustomerId={0}";
            var address = new Uri(string.Format(add, CustomerId));
            try
            {
                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<CustomerOrderResponse>(strResult); ;

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }


        public Task<object> CheckInventory(string CustomerId, string SubscriptionId, string CountryCode, string ProductId)
        {
            throw new NotImplementedException();
        }

        public async Task<AddLicensesResponse> AssignLicense(string selectedProductSkuId, string selectedCustomerId, string selectedCustomerUserId)
        {
            var address = string.Format(BaseUrlString, "license/assignlicense");

            var queryString = new Dictionary<string, string>()
{
    { "selectedProductSkuId", selectedProductSkuId},
     { "selectedCustomerId", selectedCustomerId},
     { "selectedCustomerUserId", selectedCustomerUserId}
};
            var requestUri = QueryHelpers.AddQueryString(address, queryString);
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            try
            {
                using (var response = await Client.SendAsync(request))
                {
                    var strResult = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<AddLicensesResponse>(strResult);

                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }



        public Task<object> GetLicensesForUserLicenseGroup(string CustomerId, string CustomerUserId)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetOfferAddOns(string OfferId)
        {
            throw new NotImplementedException();
        }

        public Task<object> ChangeQuantity(string CustomerId, string SubscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<object> TrialtoPaid(string CustomerId, string SubscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAddonSubscriptions(string CustomerId, string SubscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetSubscriptionbyId(string CustomerId, string SubscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetSubscriptionProvisioningStatus(string CustomerId, string SubscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetSubscriptionRegStatus(string CustomerId, string SubscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<object> ReactivateSubscription()
        {
            throw new NotImplementedException();
        }

        public Task<object> RegisterSubscription(string CustomerId, string SubscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<object> Suspendsubscription(SubscriptionModel sub, string Subscription)
        {
            throw new NotImplementedException();
        }

        public Task<object> Transitionsubscription(TargetOfferModel model, string SubscriptionId, string customerId)
        {
            throw new NotImplementedException();
        }

        public Task<object> CreateOrder(OrderModel model, string CustomerId)
        {
            throw new NotImplementedException();
        }

        public Task<object> CreateOrderForIndirectReseller(OrderModel model, string CustomerId, string ResellerId)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetOrderbyId(string CustomerId, string OrderId)
        {
            throw new NotImplementedException();
        }

        public Task<object> PurchaseSubscriptionAddOn(OrderModel model, string CustomerId, string SubdcriptionId)
        {
            throw new NotImplementedException();
        }


    }

}