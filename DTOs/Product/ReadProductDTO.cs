using belajarASPDotnetAPI.Models;

namespace belajarASPDotnetAPI.DTOs.Product
{
    public class ReadProductDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }
}
