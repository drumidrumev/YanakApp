using YanakApp.DTOs.User;

namespace YanakApp.Contracts
{
    public interface IUsersService
    {
        Task<GetUserDto> CreateUserAsync(CreateUserDto createDto);
        Task DeleteUserAsync(int id);
        Task<GetUserDto?> GetUserAsync(int id);
        Task<IEnumerable<GetUsersDto>> GetUsersAsync();
        Task UpdateUserAsync(int id, UpdateUserDto updateDto);
        Task UserBuyAsync(int userId, int productId);
        Task<bool> UserExistsAsync(int id);
    }
}