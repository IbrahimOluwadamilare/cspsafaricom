using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models
{
    public class EmailSettings
    {
        public string PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; }

        public string UsernameEmail { get; set; }

        public string UsernamePassword { get; set; }

        public string FromEmail { get; set; }

      
    }
}
