using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Configuration
{
    public static class Config
    {


        internal static string Email = "csp@wragbysolutions.com";

        internal static string JobTitle = "Admin";
        internal static string FirstName = "Wragby";
        internal static string LastName = "Solutions";
        internal static string Middle = "Business";
        internal static string Organization = "Wragby Business Solutions";
        internal static string Phone = "+234.9032349593";
        internal static string Address = "21a, Olubunmi Rotimi Street, Lekki";
        internal static string PostalCode = "105102";
        internal static string City = "Lekki";
        internal static string State = "Lagos";
        internal static string Country = "NG";
        internal static string merchant_secret = "x5UWyrYHTuXBa8oAHhaGe1Y2kfyBV1RjqrSbYDNPP5B2OTDjwH";
        internal static string merchant_code = "3172";

        internal static string FirstCheckout = "FirstCheckout";
        internal static string Paystack = "Paystack";
        internal static string SupportMail = "SPOC@wragbysolutions.com";
        internal static string OrderLink = "https://csp.wragbysolutions.com/CustomerDashboard/OrderDetails";

        internal static string FrontEndUrl => Environment.GetEnvironmentVariable("FRONTEND_URL");
    }
}
