using System.ComponentModel.DataAnnotations;

namespace CSBGlobal.Models
{
    public class Cart
    {

        [Key]
        public int RecordId { get; set; }
        public string ProductId { get; set; }
        public string CartId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual ProductOffering Product { get; set; }

    }
}
