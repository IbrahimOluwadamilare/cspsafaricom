namespace cspv3.Models.CspApiModels
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CustomersModel
    {
        [JsonProperty("_NewCompanyProfile")]
        public NewCompanyProfile NewCompanyProfile { get; set; }

        [JsonProperty("_CustomerBillingProfile")]
        public CustomerBillingProfile CustomerBillingProfile { get; set; }
    }

    public partial class CustomerBillingProfile
    {
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("Address")]
        public Address Address { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("AddressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("PostalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }

    public partial class NewCompanyProfile
    {
        [JsonProperty("DomainInput")]
        public string DomainInput { get; set; }
    }

    public partial class CustomersModel
    {
        public static CustomersModel FromJson(string json) => JsonConvert.DeserializeObject<CustomersModel>(json, cspv3.Models.CspApiModels.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CustomersModel self) => JsonConvert.SerializeObject(self, cspv3.Models.CspApiModels.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
