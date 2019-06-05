using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace CSBGlobal.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {



        [Required]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]

        [MaxLength(100)]
        public string LastName { get; set; }




        [Required]
        public byte Level { get; set; }

        [MaxLength(100)]
        public string CspId { get; set; }

        [MaxLength(100)]
        public string CompanyAddress { get; set; }

        [MaxLength(100)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string UserAddress { get; set; }

        [Required]
        public DateTime RegisterationDate { get; set; }

        public string Domain { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]

        public string PostCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public string BillingProfileId { get; set; }
        public string Etag { get; set; }
        public string CspDefaultUserName { get; set; }
        public string CspDefaultPassword { get; set; }
        public string Url { get; set; }
        public bool HasAgreement { get; set; }
    }
}
