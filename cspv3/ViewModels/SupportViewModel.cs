using cspv3.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.ViewModels
{
    public class SupportViewModel
    {
        public IFormFile File { get; set; }
        public int Id { get; set; }
        public string Department { get; set; }
        public string Priority { get; set; }

        [Required, MaxLength(80)]
        [Display(Name = "Subject")]

        public string Subject { get; set; }
        [Display(Name = "Description")]

        [Required, MaxLength(200)]

        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateResolved { get; set; }
        public string CaseOwner { get; set; }
        public string Attachment { get; set; }
    }
}
