using System;
using System.Net.Http;
using System.Threading.Tasks;
using cspv3.Configuration;
using cspv3.Helpers;
using cspv3.Models;
using Newtonsoft.Json;

namespace cspv3.Services
{
    public class FirstCheckoutService : IFirstCheckout
    {
        public string apiEndpointUrl = "https://bespoke.firstchekout.com/api/transactions/";
        private HttpClient Client { get; }
        public FirstCheckoutService()
        {
            Client = new HttpClient();
            Client.Timeout = TimeSpan.FromSeconds(1020);
            Client.DefaultRequestHeaders.Add("Api-Key", Config.merchant_secret);

        }

        public async Task<FirstCheckoutResponse> TransactionVerification(string referenceId)
        {
            var referenceIdhash = GetHash.SHA512Hash(referenceId, Config.merchant_secret, Config.merchant_code);
            var url = apiEndpointUrl +  referenceId + "/" + referenceIdhash + "/" + "query";

            try
            {
                using (var response = await Client.GetAsync(url))
                {
                    var strResult = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {


                        return JsonConvert.DeserializeObject<FirstCheckoutResponse>(strResult);

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
