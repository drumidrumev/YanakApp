using YanakApp.DTOs.Product;

namespace YanakApp.DTOs.User
{
    public record GetUserDto 
    ( 
       int Id,
       string FirstName,
       string LastName,
       string Email,
       List<GetProductSlimDto> Products
    );
}
