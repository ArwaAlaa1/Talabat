using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTO
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
       
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string PassWord { get; set; }

    }
}
