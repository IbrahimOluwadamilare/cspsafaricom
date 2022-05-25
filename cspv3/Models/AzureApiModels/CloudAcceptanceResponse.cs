using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels.Acceptance
{
    public class CloudAcceptanceResponse
    {
        public string UserId { get; set; }
        public PrimaryContact PrimaryContact { get; set; }
        public string TemplateId { get; set; }
        public DateTimeOffset DateAgreed { get; set; }
        public string Type { get; set; }
        public object AgreementLink { get; set; }
    }

    public partial class PrimaryContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
namespace cspv3.Models.AzureApiModels.AcceptanceResponse
{
    

    public class CloudConfirmationResponse
    {
        [JsonProperty("TotalCount")]
        public long TotalCount { get; set; }

        [JsonProperty("Items")]
        public List<Item> Items { get; set; }

        [JsonProperty("Links")]
        public Links Links { get; set; }

        [JsonProperty("Attributes")]
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        [JsonProperty("ObjectType")]
        public string ObjectType { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("UserId")]
        public object UserId { get; set; }

        [JsonProperty("PrimaryContact")]
        public PrimaryContact PrimaryContact { get; set; }

        [JsonProperty("TemplateId")]
        public string TemplateId { get; set; }

        [JsonProperty("DateAgreed")]
        public DateTimeOffset DateAgreed { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("AgreementLink")]
        public Uri AgreementLink { get; set; }
    }

    public partial class PrimaryContact
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("Self")]
        public Self Self { get; set; }
    }

    public partial class Self
    {
        [JsonProperty("Uri")]
        public string Uri { get; set; }

        [JsonProperty("Method")]
        public string Method { get; set; }

        [JsonProperty("Headers")]
        public List<object> Headers { get; set; }
    }
}

   