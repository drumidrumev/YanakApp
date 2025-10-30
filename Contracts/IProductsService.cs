using YanakApp.DTOs.Product;

namespace YanakApp.Contracts
{
    public interface IProductsService
    {
        Task<GetProductDto> CreateProductAsync(CreateProductDto createDto);
        Task DeleteProductAsync(int id);
        Task<GetProductDto?> GetProductAsync(int id);
        Task<IEnumerable<GetProductsDto>> GetProductsAsync();
        Task UpdateProductAsync(int id, UpdateProductDto updateDto);
        Task<bool> ProductExistsAsync(int id);
    }
}