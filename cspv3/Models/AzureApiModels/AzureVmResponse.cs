using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.AzureApiModels.VmResponse
{
    public partial class AzureVmResponse
    {
        public string Locale { get; set; }
        public string Currency { get; set; }
        public bool IsTaxIncluded { get; set; }
        public List<Meter> Meters { get; set; }
        public List<OfferTerm> OfferTerms { get; set; }
        public Attributes Attributes { get; set; }
    }

    public partial class Attributes
    {
        public string ObjectType { get; set; }
    }

    public partial class Meter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, double> Rates { get; set; }
        public List<string> Tags { get; set; }
        public CspApiModels.Target.Category Category { get; set; }
        public string Subcategory { get; set; }
        public string Region { get; set; }
        public string Unit { get; set; }
        public int IncludedQuantity { get; set; }
        public DateTimeOffset EffectiveDate { get; set; }
    }

    public partial class OfferTerm
    {
        public string Name { get; set; }
        public double Discount { get; set; }
        public List<string> ExcludedMeterIds { get; set; }
        public DateTimeOffset EffectiveDate { get; set; }
    }





}
