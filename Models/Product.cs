using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace belajarASPDotnetAPI.Models
{
    public class Product
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
    }
}
