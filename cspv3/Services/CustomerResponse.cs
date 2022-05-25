using System;
using System.Collections.Generic;

namespace cspv3.Services.CResponse
{
    public partial class CustomerResponse
    {
        public Guid Id { get; set; }
        public Guid CommerceId { get; set; }
        public CompanyProfile CompanyProfile { get; set; }
        public BillingProfile BillingProfile { get; set; }
        public string RelationshipToPartner { get; set; }
        public bool AllowDelegatedAccess { get; set; }
        public object UserCredentials { get; set; }
        public List<string> CustomDomains { get; set; }
        public object AssociatedPartnerId { get; set; }
        public Links Links { get; set; }
        public CustomerResponseAttributes Attributes { get; set; }
    }

    public partial class CustomerResponseAttributes
    {
        public string ObjectType { get; set; }
    }

    public partial class BillingProfile
    {
        public Guid Id { get; set; }
        public object FirstName { get; set; }
        public object LastName { get; set; }
        public string Email { get; set; }
        public string Culture { get; set; }
        public string Language { get; set; }
        public long CompanyName { get; set; }
        public DefaultAddress DefaultAddress { get; set; }
        public Links Links { get; set; }
        public BillingProfileAttributes Attributes { get; set; }
    }

    public partial class BillingProfileAttributes
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

    public partial class CompanyProfile
    {
        public Guid TenantId { get; set; }
        public string Domain { get; set; }
        public long CompanyName { get; set; }
        public Links Links { get; set; }
        public CustomerResponseAttributes Attributes { get; set; }
    }

}