using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace cspv3.Models.DomainModelse
{
    public partial class DomainPurchaseModel
    {
        [JsonProperty("consent")]
        public Consent Consent { get; set; }

        [JsonProperty("contactAdmin")]
        public Contact ContactAdmin { get; set; }

        [JsonProperty("contactBilling")]
        public Contact ContactBilling { get; set; }

        [JsonProperty("contactRegistrant")]
        public Contact ContactRegistrant { get; set; }

        [JsonProperty("contactTech")]
        public Contact ContactTech { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("nameServers")]
        public List<string> NameServers { get; set; }

        [JsonProperty("period")]
        public int Period { get; set; }

        [JsonProperty("privacy")]
        public bool Privacy { get; set; }

        [JsonProperty("renewAuto")]
        public bool RenewAuto { get; set; }
    }

    public partial class Consent
    {
        [JsonProperty("agreedAt")]
        public string AgreedAt { get; set; }

        [JsonProperty("agreedBy")]
        public string AgreedBy { get; set; }

        [JsonProperty("agreementKeys")]
        public List<string> AgreementKeys { get; set; }
    }

    public partial class Contact
    {
        [JsonProperty("addressMailing")]
        public AddressMailing AddressMailing { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("nameFirst")]
        public string NameFirst { get; set; }

        [JsonProperty("nameLast")]
        public string NameLast { get; set; }

        [JsonProperty("nameMiddle")]
        public string NameMiddle { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }

    public partial class AddressMailing
    {
        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }

}
