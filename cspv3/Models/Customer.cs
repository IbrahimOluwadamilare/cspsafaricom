using System;
namespace cspv3.Models
{
    public class Customer
    {
        public string CspId { get; set; }
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserAddress { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Country { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Level { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
