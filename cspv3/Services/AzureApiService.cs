using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using cspv3.Models;
using cspv3.Models.AzureApiModels;
using cspv3.Models.AzureApiModels.Acceptance;
using cspv3.Models.AzureApiModels.AcceptanceResponse;
using cspv3.Models.AzureApiModels.ASubResponse;
using cspv3.Models.AzureApiModels.Availability;
using cspv3.Models.AzureApiModels.CartResponse;
using cspv3.Models.AzureApiModels.PResponse;
using cspv3.Models.AzureApiModels.SingleAvail;
using cspv3.Models.AzureApiModels.SingleSku;
using cspv3.Models.AzureApiModels.SkuResponse;
using cspv3.Models.AzureApiModels.SpResponse;
using cspv3.Models.AzureApiModels.VmResponse;
using cspv3.Models.Checkout;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cspv3.Services
{
    public class AzureApiService : IAzureApi
    {
        public const string BaseUrlString = "https://smeproductivitycsbapi.azurewebsites.net/api/v3/{0}";

        private HttpClient Client { get; }

        public AzureApiService()
        {
            Client = new HttpClient();
            Client.Timeout = TimeSpan.FromSeconds(1020);
        }
        public async Task<InventoryResponse> CheckInventory(string CustomerId, string SubscriptionId, string productId, string skuId)
        {
            try
            {


                var address = string.Format(BaseUrlString, "inventory/checkinventory");
                var queryString = new Dictionary<string, string>()
{
    { "customerId", CustomerId},
    { "SubscriptionId", SubscriptionId},
     { "productId", productId},
      { "skuId", skuId}
};
                var requestUri = QueryHelpers.AddQueryString(address, queryString);

                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                using (var response = await Client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<InventoryResponse>(strResult);

                        return result;

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public async Task<ProductAvailabilityResponse> GetAvailabilities(string ProductId, string SkuId)
        {
            try { 
            var address = string.Format(BaseUrlString, "products/getavailabilities");
            var queryString = new Dictionary<string, string>()
{
    { "ProductId", ProductId},
    { "SkuId", SkuId}, 
};
            var requestUri = QueryHelpers.AddQueryString(address, queryString);
            using (var response = await Client.GetAsync(requestUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    var strResult = await response.Content.ReadAsStringAsync();


                    return JsonConvert.DeserializeObject<ProductAvailabilityResponse>(strResult); ;

                }
                return null;
            }
        }
            catch (System.Exception e)
            {
                return null;
            }

        }

        public Task<SingleAvailResponse> GetAvailabilitiesbyId(string productId, string SkuId, string AvailabilityId)
        {
            throw new NotImplementedException();
        }

        public async Task<AzureVmResponse> GetAzureVmProductsAsync()
        {
            try
            {


                var address = new Uri(string.Format(BaseUrlString, "ratecard/getazureratecard"));


                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<AzureVmResponse>(strResult);

                        return result;

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public async Task<SubcriptionResponse> GetCustomerSubscriptionsAsync(string CustomerId)
         {

            try
            {
                var address = string.Format(BaseUrlString, "subscription/getcustomersubscriptions");


                var queryString = new Dictionary<string, string>()
{
    { "customerId", CustomerId},
};
                var requestUri = QueryHelpers.AddQueryString(address, queryString);


                using (var response = await Client.GetAsync(requestUri))
                    {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<SubcriptionResponse>(strResult); ;

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public Task<SingleProductResponse> GetProductbyId(string ProductId)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductsResponse> Getproducts()
        {
            try
            {


                var address = new Uri(string.Format(BaseUrlString, "products/getproducts"));


                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<ProductsResponse>(strResult); ;

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public Task<SingleSkuResponse> GetSKUbyId(string ProductId, string SkuId)
        {
            throw new NotImplementedException();
        }

        public async Task<SkuResponse> GetSKUs(string ProductId)
        {
            try
            {
                var add = "https://smeproductivitycsbapi.azurewebsites.net/api/v3/products/getskus?productId={0}";

                // var address = new Uri(string.Format(BaseUrlString, "products/getskus"));

                var address = new Uri(string.Format(add, ProductId));

                using (var response = await Client.GetAsync(address))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<SkuResponse>(strResult); ;

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public Task<SingleSubResponse> GetSubscriptionbyId(string SubscriptionId, string CustomerId)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateCartResponse> CreateCart(string CustomerId, string CatalogItemId, string SubscriptionId, CartLineItems cart)
        
        {

                var address = string.Format(BaseUrlString, "cart/createcart");
                try
                {
                    var queryString = new Dictionary<string, string>()
{
    { "customerId", CustomerId},
        { "subscriptionId", SubscriptionId},
    { "catalogItemId", CatalogItemId}

};

                    var requestUri = QueryHelpers.AddQueryString(address, queryString);


                   
                    var payload = JsonConvert.SerializeObject(cart);
                var json = "[" + payload + "]";
                var content = new StringContent(json, Encoding.UTF8, "application/json");
            
     

                    // Send the request
                    using (var response = await Client.PostAsync(requestUri, content))
                    {
                    var str = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                        {
                            var strResult = await response.Content.ReadAsStringAsync();


                            return JsonConvert.DeserializeObject<CreateCartResponse>(strResult);

                        }
                        return null;
                    }
                }
                catch (System.Exception e)
                {
                    return null;
                }
            }

        public async Task<CheckoutCartResponse> Checkout(string CustomerId, string cartId)
        {
            var address = string.Format(BaseUrlString, "cart/checkout");
            try
            {
                var queryString = new Dictionary<string, string>()
{
    { "customerId", CustomerId},
        { "cartId", cartId},

};

                var requestUri = QueryHelpers.AddQueryString(address, queryString);


                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                // Send the request
                using (var response = await Client.SendAsync(request))
                {
                    var str = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<CheckoutCartResponse>(strResult);

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public async Task<CloudAcceptanceResponse> ConfirmCloudAcceptance(string CustomerId, string FirstName, string LastName, string Email, string Phone)
        {
            var address = string.Format(BaseUrlString, "customer/confirmcloudacceptance");
            try
            {
                var queryString = new Dictionary<string, string>()
{
    { "selectedCustomerId", CustomerId},
        { "firstName", FirstName},
         { "lastName", LastName},
                    { "email", Email},
                    { "phone", Phone},

};

                var requestUri = QueryHelpers.AddQueryString(address, queryString);


                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                // Send the request
                using (var response = await Client.SendAsync(request))
                {
                    var str = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<CloudAcceptanceResponse>(strResult);

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public async Task<CloudConfirmationResponse> GetConfirmationofCloudagreement(string CustomerId)
        {
            var address = string.Format(BaseUrlString, "customer/getconfirmationofmicrosoftcloudagreement");
            try
            {
                var queryString = new Dictionary<string, string>()
{
    { "selectedCustomerId", CustomerId},
      

};

                var requestUri = QueryHelpers.AddQueryString(address, queryString);


              
                using (var response = await Client.GetAsync(requestUri))
                {
                    var str = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<CloudConfirmationResponse>(strResult);

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public async Task<SingleSubResponse> SuspendSubscriptionAsync(string CustomerId, string SubscriptionId)
        {
            var address = string.Format(BaseUrlString, "subscription/suspendsubscription");
            try
            {
                var queryString = new Dictionary<string, string>()
{
    { "selectedCustomerId", CustomerId},
    { "selectedSubscription", SubscriptionId}


};

                var requestUri = QueryHelpers.AddQueryString(address, queryString);

                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);


                using (var response = await Client.SendAsync(request))
                {
                    var str = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<SingleSubResponse>(strResult);

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public async Task<SingleSubResponse> ReactivateSubscriptionAsync(string CustomerId, string SubscriptionId)
        {
            var address = string.Format(BaseUrlString, "subscription/reactivatesubscription");
            try
            {
                var queryString = new Dictionary<string, string>()
{
    { "customer", CustomerId},
    { "selectedSubscription", SubscriptionId}


};

                var requestUri = QueryHelpers.AddQueryString(address, queryString);
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);



                using (var response = await Client.SendAsync(request))
                {
                    var str = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<SingleSubResponse>(strResult);

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public async Task<SubcriptionResponse> GetSubscriptionbyOrderAsync(string CustomerId, string OrderId)
        {
            var address = string.Format(BaseUrlString, "subscription/getsubscriptionbyorder");
            try
            {
                var queryString = new Dictionary<string, string>()
{
    { "selectedCustomerId", CustomerId},
    { "OrderId", OrderId}


};

                var requestUri = QueryHelpers.AddQueryString(address, queryString);



                using (var response = await Client.GetAsync(requestUri))
                {
                    var str = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<SubcriptionResponse>(strResult);

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }
    }
}







