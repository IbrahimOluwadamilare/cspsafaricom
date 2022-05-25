using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace cspv3.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Licences")]
        public string Licences { get; set; }

        [Required]
        [Remote("Domaincheck", "Home", HttpMethod = "POST", ErrorMessage = "EmailId already exists in database.")]
        [Display(Name = "Domain")]
        public string Domain { get; set; }
    }
}