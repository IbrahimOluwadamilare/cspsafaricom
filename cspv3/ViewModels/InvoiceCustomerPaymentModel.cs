using System.ComponentModel.DataAnnotations;

namespace cspv3.ViewModels
{
    public class InvoiceCustomerPaymentModel
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

        [Display(Name = "Notes")]
        [DataType(DataType.Text)]
        public string Notes { get; set; }

        public decimal payment { get; set; }
    }
}