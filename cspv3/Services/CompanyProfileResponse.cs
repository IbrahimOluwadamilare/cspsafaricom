using System;
using System.Collections.Generic;

namespace cspv3.Services.CProfile
{
    public partial class CompanyProfileResponse
    {
        public Guid TenantId { get; set; }
        public string Domain { get; set; }
        public long CompanyName { get; set; }
        public Links Links { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string ObjectType { get; set; }
    }

    public partial class Links
    {
        public Self Self { get; set; }
    }

    public partial class Self
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public List<object> Headers { get; set; }
    }

}