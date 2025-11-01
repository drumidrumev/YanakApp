using Microsoft.EntityFrameworkCore;
using YanakApp.Contracts;
using YanakApp.DTOs.Product;
using YanakApp.Models;

namespace YanakApp.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ShopDbContext _context;
        public ProductsService(ShopDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<GetProductsDto>> GetProductsAsync()
        {
            var products = await _context.Products
                        .Select(pr => new GetProductsDto(pr.Name, pr.Description, pr.UserId))
                        .ToListAsync();
            return products;

        }

        public async Task<GetProductDto?> GetProductAsync(int id)
        {
            var product = await _context.Products
                .Where(el => el.Id == id)
                .Select(pr => new GetProductDto(
                    pr.Id,
                    pr.Name,
                    pr.Description,
                    pr.User!.LastName
                    ))
                .FirstOrDefaultAsync();

            return product ?? null;

        }


        public async Task<GetProductDto> CreateProductAsync(CreateProductDto createDto)
        {
            var user = await _context.Users.FindAsync(createDto.UserId);

            var product = new Product
            {
                Name = createDto.Name,
                Description = createDto.Description,
                UserId = createDto.UserId,

            };


            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var result = new GetProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.User!.LastName
                );

            return result;


        }


        public async Task UpdateProductAsync(int id, UpdateProductDto updateDto)
        {
            var product = await _context.Products.FindAsync(id);

    
            product.Name = updateDto.Name;
            product.Description = updateDto.Description;
            product.UserId = updateDto.UserId;

            _context.Entry(product).State = EntityState.Modified;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();


        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id) ?? throw new KeyNotFoundException("Product not found");
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }

    }
}
