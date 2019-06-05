using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSBGlobal.Models.CspApiModels.AddLicense
{
    public partial class AddLicensesResponse
    {
        public List<LicensesToAssign> LicensesToAssign { get; set; }
        public object LicensesToRemove { get; set; }
        public List<object> LicenseWarnings { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string ObjectType { get; set; }
    }

    public partial class LicensesToAssign
    {
        public object ExcludedPlans { get; set; }
        public string SkuId { get; set; }
    }

}
