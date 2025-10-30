using System.ComponentModel.DataAnnotations;

namespace YanakApp.DTOs.Product
{
    public class UpdateProductDto : CreateProductDto 
    {
        [Required]
        public int Id { get; set; }

    }


}
