using System;
using System.Collections.Generic;

namespace CSBGlobal.Models.CspApiModels.CustomerResponseModel
{
    public partial class CustomersResponseModel
    {
        public string Id { get; set; }
        public string CommerceId { get; set; }
        public CompanyProfile CompanyProfile { get; set; }
        public BillingProfile BillingProfile { get; set; }
        public string RelationshipToPartner { get; set; }
        public bool AllowDelegatedAccess { get; set; }
        public UserCredentials UserCredentials { get; set; }
        public List<string> CustomDomains { get; set; }
        public string AssociatedPartnerId { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string Etag { get; set; }
        public string ObjectType { get; set; }
    }

    public partial class BillingProfile
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Culture { get; set; }
        public string Language { get; set; }
        public string CompanyName { get; set; }
        public DefaultAddress DefaultAddress { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class DefaultAddress
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
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
        public List<Header> Headers { get; set; }
    }

    public partial class Header
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public partial class CompanyProfile
    {
        public string TenantId { get; set; }
        public string Domain { get; set; }
        public string CompanyName { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class UserCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
