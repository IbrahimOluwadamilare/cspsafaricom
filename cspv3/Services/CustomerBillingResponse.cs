using System;
using System.Collections.Generic;

namespace cspv3.Services.CBilling
{
    public partial class CustomerBillingResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Culture { get; set; }
        public string Language { get; set; }
        public long CompanyName { get; set; }
        public DefaultAddress DefaultAddress { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string Etag { get; set; }
        public string ObjectType { get; set; }
    }

    public partial class DefaultAddress
    {
        public string Country { get; set; }
        public object Region { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AddressLine1 { get; set; }
        public object AddressLine2 { get; set; }
        public long PostalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }

    public partial class Links
    {
        public Self Self { get; set; }
    }

    public partial class Self
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public List<object> Headers { get; set; }
    }

}