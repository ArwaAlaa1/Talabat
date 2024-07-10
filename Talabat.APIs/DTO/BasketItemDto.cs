using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTO
{
    public class BasketItemDto
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UrlPicture { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(0.1, double.MaxValue,ErrorMessage ="Price Must Be Greater Than Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must Be One Item At Least")]
        public int Quantity { get; set; }
    }
}
