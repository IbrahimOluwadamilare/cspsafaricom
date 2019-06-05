using System.ComponentModel.DataAnnotations;

namespace CSBGlobal.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password (UpperCase, LowerCase, Number/SpecialChar and min 8 Chars)")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Csp Id")]
        public string CspId { get; set; }

        [Display(Name = "Domain")]
        public string Domain { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Company Address")]
        public string CompanyAddress { get; set; }

        [Required]
        [Display(Name = "User Address")]
        public string UserAddress { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\+(?:[0-9]●?){6,14}[0-9]$", ErrorMessage = "Please Enter Valid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Postal Code")]

        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Please Enter Valid Postal Code.")]
        public string PostCode { get; set; }

        [Required]

        [Display(Name = "State")]
        public string State { get; set; }

        [Required]

        [Display(Name = "City")]
        public string City { get; set; }

        [Required]

        [Display(Name = "Country")]
        public string Country { get; set; }


    }
}
