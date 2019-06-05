using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Models
{
    public class ContactSupport
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public string Email { get; set; }
        public string Phone { get; set; }


        [Required, MaxLength(80)]
        [Display(Name = "Subject")]

        public string Subject { get; set; }
        [Display(Name = "Message")]

        [Required, MaxLength(200)]

        public string Message { get; set; }
        [Display(Name = "Date Created")]

        public string DateCreated { get; set; }


    }
}
