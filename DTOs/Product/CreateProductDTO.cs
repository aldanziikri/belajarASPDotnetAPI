using belajarASPDotnetAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace belajarASPDotnetAPI.DTOs.Product
{
    public class CreateProductDTO
    {

        [Required(ErrorMessage = "Nama produk wajib diisi.")]
        public string Name { get; set; }
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Kuantitas produk wajib diisi.")]
        [Range(0, 999999, ErrorMessage = "Kuantitas harus berisi minimal 0")]
        public int Quantity { get; set; }

        [MaxLength(50, ErrorMessage = "Deskripsi tidak boleh melebihi 50 karakter")]
        public string Description { get; set; }


    }
}
