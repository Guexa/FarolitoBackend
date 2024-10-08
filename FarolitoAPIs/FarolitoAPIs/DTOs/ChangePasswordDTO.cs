﻿using System.ComponentModel.DataAnnotations;

namespace FarolitoAPIs.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
