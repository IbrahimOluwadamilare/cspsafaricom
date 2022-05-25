using cspv3.Helpers;
using cspv3.Models;
using cspv3.Models.CspApiModels;
using cspv3.Models.CspApiModels.Offers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace cspv3.Services
{
    public class OffersService
    {
        public async Task<AllOffersModel> GetOffers()
        {
            try
            {



                HttpClient client = new HttpClient();
                var BaseAddress = new Uri(AppConstants.allOffers);
                var httpResponse = await client.GetAsync(BaseAddress);

                //.Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    var data = await httpResponse.Content.ReadAsStringAsync();

                    var allOffers = JsonConvert.DeserializeObject<AllOffersModel>(JObject.Parse(data).ToString());


                    return allOffers;

                }
                else
                {
                    return null;
                }
                //allOffers = JsonConvert.DeserializeObject<List<Offer>>(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }

}
