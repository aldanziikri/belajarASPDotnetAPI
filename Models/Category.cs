using System.Text.Json.Serialization;

namespace belajarASPDotnetAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Category_Name { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
