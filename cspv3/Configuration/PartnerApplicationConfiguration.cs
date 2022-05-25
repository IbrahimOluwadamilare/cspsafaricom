using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace cspv3.Configuration
{
    public static class PartnerApplicationConfiguration
    {
        public static string PartnerServiceApiRoot = "https://api.partnercenter.microsoft.com";
        public static string Authority = "https://login.windows.net";
        public static string ResourceUrl = "https://graph.windows.net";
        public static string ApplicationId = "8b2dba71-2335-4789-b125-bb3beb45334e";
        public static string ApplicationSecret = "GzMti/Cbn4OMlM6vjtgWxriWtpEWkz5MH35pG/Z1/UI=";
        public static string ApplicationDomain = "wragbysolutions.com";


     
    }

    public static class PartnerUserConfiguration
    {
        public static string PartnerServiceApiRoot = "https://api.partnercenter.microsoft.com";
        public static string Authority = "https://login.windows.net";
        public static string UserName = "cspadmin@wragbysolutions.com";
        public static string Password = "$wbstElephant1";
        public static string ResourceUrl = "https://api.partnercenter.microsoft.com";
        public static string ClientId = "8b2dba71-2335-4789-b125-bb3beb45334e";
        public static string ApplicationDomain = "wragbysolutions.com";
    }



    public static class GetAuthToken
    {
        public static JObject GetADToken(string resellerDomain, string clientId, string clientSecret)
        {
            var request = WebRequest.Create(
                string.Format(
                    "{0}/{1}/oauth2/token",
                    PartnerApplicationConfiguration.Authority,
                    resellerDomain));

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string content = string.Format(
                "grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}",
                clientId,
                HttpUtility.UrlEncode(clientSecret),
                HttpUtility.UrlEncode(PartnerApplicationConfiguration.ResourceUrl));

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(content);
            }

            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseContent = reader.ReadToEnd();
                    var adResponse =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseContent);
                    return adResponse;
                }

            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    using (var reader = new StreamReader(webException.Response.GetResponseStream()))
                    {
                        var responseContent = reader.ReadToEnd();
                    }
                }
            }

            return null;
        }


        public static JObject GetADToken(string resellerDomain, string clientId)
        {
            string loginUrl =
                string.Format(
                    "{0}/{1}/oauth2/token",
                    PartnerUserConfiguration.Authority,
                    resellerDomain);

            var request = WebRequest.Create(loginUrl);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            string content = string.Format(
                "resource={0}&client_id={1}&grant_type=password&username={2}&password={3}&scope=openid",
                HttpUtility.UrlEncode(PartnerUserConfiguration.ResourceUrl),
                HttpUtility.UrlEncode(clientId),
                HttpUtility.UrlEncode(PartnerUserConfiguration.UserName),
                HttpUtility.UrlEncode(PartnerUserConfiguration.Password));

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(content);
            }

            try
            {
                var response = request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseContent = reader.ReadToEnd();
                    var adResponse =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseContent);
                    return adResponse;
                }

            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    using (var reader = new StreamReader(webException.Response.GetResponseStream()))
                    {
                        var responseContent = reader.ReadToEnd();
                    }
                }
            }

            return null;
        }
    }
}
