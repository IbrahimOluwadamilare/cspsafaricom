using System.ComponentModel.DataAnnotations;

namespace cspv3.ViewModels
{
    public class LoginViewModel
    {
        //[Required]
        [DataType(DataType.Text)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        //[Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}