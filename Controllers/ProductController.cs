using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using belajarASPDotnetAPI.Data;
using belajarASPDotnetAPI.Models;

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
                

                List<Product> products = await _context.Products.ToListAsync();
                return Ok(new
                {
                    status_code = 200,
                    message = "List data product",
                    data = products
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    status_code = 500,
                    message = "Gagal mengambil data product"
                });
            }

        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> PostProduct(Product product)
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
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProductById", new { product.id }, new
                {
                    status_code = 201,
                    message = "Berhasil menambahkan product",
                    data = product
                });
            }
            catch(Exception)
            {
                return StatusCode(500, new
                {
                    status_code = 500,
                    message = "Gagal terhubung ke server"
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById(int id)
        {
            try
            {
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
        public async Task<ActionResult<IEnumerable<Product>>> UpdateProduct(int id, Product newData)
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
                product.name = newData.name;
                product.quantity = newData.quantity;
                product.description = newData.description;

                await _context.SaveChangesAsync();
                return Ok(new
                {
                    status_code = 200,
                    message = "Produk berhasil diperbarui",
                    data = product
                });

            }
            catch
            {
                return StatusCode(500, new
                {
                    message = "Gagal terhubung ke server",
                    status_code = 500
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
