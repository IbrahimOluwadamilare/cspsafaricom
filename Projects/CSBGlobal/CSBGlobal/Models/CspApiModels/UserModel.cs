using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Models.CspApiModels
{
    public partial class UserModel
    {
        public string UsageLocation { get; set; }
        [Required]

        public string UserPrincipalName { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string DisplayName { get; set; }
        public PasswordProfile PasswordProfile { get; set; }

    }

  
   

    public partial class PasswordProfile
    {
        public bool ForceChangePassword { get; set; }
        public string Password { get; set; }
    }
}
