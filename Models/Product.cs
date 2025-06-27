using System.ComponentModel.DataAnnotations;

namespace belajarASPDotnetAPI.Models
{
    public class Product
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Nama produk wajib diisi.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Kuantitas produk wajib diisi.")]
        [Range(0,999999, ErrorMessage = "Kuantitas harus berisi minimal 0")]
        public int quantity { get; set; }

        [MaxLength(50, ErrorMessage = "Deskripsi tidak boleh melebihi 50 karakter")]
        public string description { get; set; }
    }
}
