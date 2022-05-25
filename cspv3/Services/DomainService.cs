using cspv3.Models.DomainModels;
using cspv3.Models.DomainModelse;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cspv3.Services
{
    public class DomainService : IDomainService
    {

        public string apiEndpointUrl = "https://api.ote-godaddy.com/";
        public DomainService()
        {
            Client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(1020),
                
        };
            Client.DefaultRequestHeaders.Add("Authorization", "sso-key 3mM44UYi3uhmsp_4dVFm4jqufLLQD73uSfR1n:4dVKMJ6DLsNwiLJhqjBCbW");
        }
        private HttpClient Client { get; }

        public async Task<ShopperIdCreationResponse> CreateSubAccountShopperId(ShopperIdCreationModel shopperIdCreationModel)
        {
           
            var url = apiEndpointUrl + "v1/shoppers/subaccount";


            var dict = new Dictionary<string, string>
            {
                { "email", shopperIdCreationModel.Email },
                { "nameFirst", shopperIdCreationModel.NameFirst },
                 { "nameLast", shopperIdCreationModel.NameLast },
                { "password", shopperIdCreationModel.Password }


            };
          
            var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dict) };
            
            try
            {

                using (var response = await Client.SendAsync(request))
                {
                    var str = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<ShopperIdCreationResponse>(strResult);

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }

        }

        public async Task<DomainOrderDetailsResponse> DomainResponse(string xShopperId)
        {
           
            Client.DefaultRequestHeaders.Add("X-Market-Id", "en-US");
            var url = apiEndpointUrl + "v1/orders/" + xShopperId;

            try
            {
                using (var response = await Client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<DomainOrderDetailsResponse>(strResult);

                    }
                    return null;
                }
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public async Task<DomainAvailableResponse> GetDomainAvailabilityAsync(string domain)
        {
           
            var url = apiEndpointUrl + "v1/domains/available?domain=" + domain + "&checkType=FAST&forTransfer=false";

            try
            {
                using (var response = await Client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<DomainAvailableResponse>(strResult);

                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public async Task<DomainPurchaseResponseModel> PurchaseDomain(DomainPurchaseModel domainPurchaseModel, string shopperId)
        {
            
            Client.DefaultRequestHeaders.Add("X-Shopper-Id", shopperId);
            var url = apiEndpointUrl + "v1/domains/purchase";

            var json = JsonConvert.SerializeObject(domainPurchaseModel);
            var demo = JObject.Parse(json);
            var content = new StringContent(demo.ToString(), Encoding.UTF8, "application/json");
            try
            {
                using (var response = await Client.PostAsync(url, content))
                {
                    var str = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var strResult = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<DomainPurchaseResponseModel>(strResult);

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
