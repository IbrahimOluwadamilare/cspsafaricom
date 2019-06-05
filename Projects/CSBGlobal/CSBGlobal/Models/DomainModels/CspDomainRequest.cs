using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Models.DomainModels
{
    public partial class CspDomainRequest
    {
        public string VerifiedDomainName { get; set; }
        public Domain Domain { get; set; }
    }

    public partial class Domain
    {
        public string AuthenticationType { get; set; }
        public string Capability { get; set; }
        public bool IsDefault { get; set; }
        public bool IsInitial { get; set; }
        public string Name { get; set; }
        public object RootDomain { get; set; }
        public string Status { get; set; }
        public string VerificationMethod { get; set; }

    }
}
