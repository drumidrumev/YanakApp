using System.ComponentModel.DataAnnotations;

namespace YanakApp.DTOs.Product
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public int? UserId { get; set; }
    }


}
