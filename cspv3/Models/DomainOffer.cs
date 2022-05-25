using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models
{
    public class DomainOffer
    {
        [Key]
        public int Id { get; set; }
        public string ShopperId { get; set; }
        public string CustomerId { get; set; }
        public string Password { get; set; }
        public string OwnerId { get; set; }
        public string DomainName { get; set; }
        public int OrderId { get; set; }
        public DateTime DateCreated { get; set; }

        public bool IsAdded { get; set; }



    }
}
