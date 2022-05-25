using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.DomainModels
{
    public partial class DomainAvailableResponse
    {
        public bool Available { get; set; }
        public string Currency { get; set; }
        public bool Definitive { get; set; }
        public string Domain { get; set; }
        public int Period { get; set; }
        public int Price { get; set; }
    }

}
