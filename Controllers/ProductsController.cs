using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YanakApp.Contracts;
using YanakApp.DTOs.Product;
using YanakApp.Models;

namespace YanakApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductsDto>>> GetProducts()
        {

            var products = await _productsService.GetProductsAsync();


            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProduct(int id)
        {
            var product = await _productsService.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest();
            }

           

            try
            {
                await _productsService.UpdateProductAsync(id,updateDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _productsService.ProductExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateProductDto createDto)
        {
            var product = await _productsService.CreateProductAsync(createDto);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productsService.DeleteProductAsync(id);

            return NoContent();
        }

    }
}
