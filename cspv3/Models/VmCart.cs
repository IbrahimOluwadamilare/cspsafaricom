using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace cspv3.Models
{
    public class VmCart
    {

        [Key]
        public int RecordId { get; set; }
        public string ProductId { get; set; }
        public string CartId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }


        public virtual SubProductOffering Productlink { get; set; }

    }
}
