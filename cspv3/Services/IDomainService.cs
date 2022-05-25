using cspv3.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using cspv3.Models.DomainModelse;

namespace cspv3.Services
{
    public interface IDomainService
    {
        Task<DomainAvailableResponse> GetDomainAvailabilityAsync(string domain);
        Task<ShopperIdCreationResponse> CreateSubAccountShopperId(ShopperIdCreationModel shopperIdCreationModel);
        Task<DomainPurchaseResponseModel> PurchaseDomain(DomainPurchaseModel domainPurchaseModel, string shopperId);
        Task<DomainOrderDetailsResponse> DomainResponse(string xShopperId);
    }
}
