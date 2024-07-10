﻿using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.DTO
{
    public class AddressDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

    }
}
