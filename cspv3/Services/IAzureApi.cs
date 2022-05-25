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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Services
{
    public interface IAzureApi
    {
        //Subscriptions
        Task<SubcriptionResponse> GetCustomerSubscriptionsAsync(string CustomerId);
        Task<SingleSubResponse> GetSubscriptionbyId(string SubscriptionId, string CustomerId);
        Task<SingleSubResponse> SuspendSubscriptionAsync(string CustomerId, string SubscriptionId);
        Task<SingleSubResponse> ReactivateSubscriptionAsync(string CustomerId, string SubscriptionId);
        Task<SubcriptionResponse> GetSubscriptionbyOrderAsync(string CustomerId, string OrderId);



        //Products
        Task<ProductsResponse> Getproducts();
        Task<SingleProductResponse> GetProductbyId(string ProductId);

        //SKUs

        Task<SkuResponse> GetSKUs(string ProductId);
        Task<SingleSkuResponse> GetSKUbyId(string ProductId, string SkuId);

        //Inventory
        Task<InventoryResponse> CheckInventory(string CustomerId, string SubscriptionId, string ProductId, string SkuId);

        //Availability
        Task<ProductAvailabilityResponse> GetAvailabilities(string ProductId, string SkuId);
        Task<SingleAvailResponse> GetAvailabilitiesbyId(string productId, string SkuId, string AvailabilityId);

        //Cart
        Task<CreateCartResponse> CreateCart(string CustomerId, string CatalogItemId, string SubscriptionId, CartLineItems cart);

        Task<CheckoutCartResponse> Checkout(string CustomerId, string cartId);

        //Azure VM
        Task<AzureVmResponse> GetAzureVmProductsAsync();

        //Cloud Agreement
        Task<CloudAcceptanceResponse> ConfirmCloudAcceptance (string CustomerId, string FirstName, string LastName, string Email, string Phone);
        Task<CloudConfirmationResponse> GetConfirmationofCloudagreement(string CustomerId);

    }

}
