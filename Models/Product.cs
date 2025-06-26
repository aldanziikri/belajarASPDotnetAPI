using System.ComponentModel.DataAnnotations;

namespace belajarASPDotnetAPI.Models
{
    public class Product
    {
        public int id { get; set; }

        //[Required]
        public string name { get; set; }
        public int quantity { get; set; }
        //[Required]
        //[MaxLength(50)]
        public string description { get; set; }
    }
}
