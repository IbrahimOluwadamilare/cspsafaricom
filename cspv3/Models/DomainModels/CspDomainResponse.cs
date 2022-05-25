using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.DomainModels
{
    public partial class CspDomainResponse
    {
        public string AuthenticationType { get; set; }
        public string Capability { get; set; }
        public bool IsDefault { get; set; }
        public bool IsInitial { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string VerificationMethod { get; set; }
    }

}
