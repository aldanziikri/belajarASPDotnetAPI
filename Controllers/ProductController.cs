using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using belajarASPDotnetAPI.Data;
using belajarASPDotnetAPI.Models;
using belajarASPDotnetAPI.DTOs.Product;

namespace belajarASPDotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {

            try
            {
                var products = await _context.Products.Include(p => p.Category).Select(p => new ReadProductDTO
                {

                    id = p.id,
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Description = p.Description,
                    CategoryName = p.Category.Category_Name,

                }).ToListAsync();
                return Ok(new
                {
                    status_code = 200,
                    message = "List data product",
                    data = products
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status_code = 500,
                    message = "Gagal mengambil data product",
                    error = ex.Message
                });
            }

        }

        [HttpPost]
        public async Task<ActionResult> PostProduct(CreateProductDTO dto)
        {
            try
            {
               if (!ModelState.IsValid)
                {
                    var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { 
                        message = "Gagal validasi",
                        status_code = 400,
                        errors = error
                    });
                }

                var product = new Product
                {
                    Name = dto.Name,
                    CategoryId = dto.CategoryId,
                    Quantity = dto.Quantity,
                    Description = dto.Description,
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProductById", new { product.id }, new
                {
                    status_code = 201,
                    message = "Berhasil menambahkan product",
                    data = product
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    status_code = 500,
                    message = "Gagal terhubung ke server",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadProductDTO>> GetProductById(int id)
        {
            try
            {
                var product = await _context.Products.Where(p => p.id == id).Select(p => new ReadProductDTO
                {
                    id = p.id,
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Description = p.Description,
                    CategoryName = p.Category.Category_Name,
                }).FirstOrDefaultAsync();

                if (product == null)
                {
                    return NotFound(new
                    {
                        status_code = 404,
                        message = "Produk tidak ditemukan"
                    }
                    );
                }
                return Ok(new
                {
                    status_code = 200,
                    message = "Data produk",
                    data = product
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Gagal terhubung ke server",
                    status_code = 500
                });
            }
            
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateProductDTO>> UpdateProduct(int id, UpdateProductDTO newData)
        {
            try
            {
                 if (!ModelState.IsValid)
                {
                    var error = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { 
                        message = "Gagal validasi",
                        status_code = 400,
                        errors = error
                    });
                }
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound(new
                    {
                        status_code = 404,
                        message = "Produk tidak ditemukan"
                    }
                    );
                }   
                product.Name = newData.Name;
                product.Quantity = newData.Quantity;
                product.CategoryId = newData.CategoryId;
                product.Description = newData.Description;

                await _context.SaveChangesAsync();
                return Ok(new
                {
                    status_code = 200,
                    message = "Produk berhasil diperbarui",
                    data = product
                });

            }
            catch (Exception ex) 
            {
                return StatusCode(500, new
                {
                    message = "Gagal terhubung ke server",
                    status_code = 500,
                    

                });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null) return NotFound(new { message = "Produk tidak ada" });

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Gagal terhubung ke server",
                    status_code = 500
                });

            }



        }

    }
}
