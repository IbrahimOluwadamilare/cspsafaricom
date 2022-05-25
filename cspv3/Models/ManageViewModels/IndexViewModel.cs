using System.ComponentModel.DataAnnotations;

namespace cspv3.Models.ManageViewModels
{
    public class IndexViewModel
    {
      

        public bool IsEmailConfirmed { get; set; }


        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }




        [Required]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        [Display(Name = "First Name")]

        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]

        [MaxLength(100)]
        public string LastName { get; set; }




      

        [MaxLength(100)]
        [Display(Name = "Company Address")]
        [Required]
        public string CompanyAddress { get; set; }

        [MaxLength(100)]
        [Display(Name = "Company Name")]
        [Required]
        public string CompanyName { get; set; }

       

     


    }
}
