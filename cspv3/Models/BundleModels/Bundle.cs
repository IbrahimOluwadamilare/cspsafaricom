using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cspv3.Models.BundleModels
{
    public class Bundle
    {
        [Key]
        public string Id { get; set; }

        [Display(Name ="Bundle Name")]
        public string BundleName { get; set; }
        //public virtual BundleCategory Category { get; set; }

    }
    
    public class BundleCategory
    {
        [Key]
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string Price { get; set; }
        public string CspId { get; set; }
        public virtual Bundle Bundle { get; set; }

         // public virtual Property Property { get; set; }
    }

    public class Property
    {

        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
     //   public virtual BundleCategory BundleCategory { get; set; }


    }
    public partial class BundleRes
    {
        [JsonProperty("Office Business Essentials")]
        public string OfficeBusinessEssentials { get; set; }

        [JsonProperty("Office 365 Business Premium")]
        public string Office365BusinessPremium { get; set; }

        [JsonProperty("Office 365 Business")]
        public string Office365Business { get; set; }
    }

    public class BundleCategories
    {
        public string CategoryName { get; set; }
        public string CspId { get; set; }

        public string Price { get; set; }


    }
 
     public class BundleVM
    {
        public List<BundleCategories> BundleCategory { get; set; }
        public string BundleName { get; set; }



    }
 

}
