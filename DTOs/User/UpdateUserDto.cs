using System.ComponentModel.DataAnnotations;

namespace YanakApp.DTOs.User
{
    public class UpdateUserDto : CreateUserDto
    {
        [Required]
        public int Id { get; set; }
    }
}
