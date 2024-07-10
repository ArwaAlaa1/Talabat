using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.APIs.DTO
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<BasketItemDto> Items { get; set; }
        public int? deliverymethodId { get; set; }
        public decimal ShippingCost { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
