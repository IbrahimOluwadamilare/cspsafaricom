using System.ComponentModel.DataAnnotations;

namespace cspv3.Models
{
    public class InvoiceCustomerModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Company Name")]
        [DataType(DataType.Text)]
        public string CompanyName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Company Address")]
        [DataType(DataType.Text)]
        public string CompanyAddress { get; set; }

        [Display(Name = "PromoCode")]
        [DataType(DataType.Text)]
        public string PromoCode { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public decimal CartTotal { get; set; }

    }
}
