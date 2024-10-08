﻿using System.ComponentModel.DataAnnotations;

namespace FarolitoAPIs.DTOs
{
    public class ProveedorEstatusDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool? Estatus { get; set; }
    }
}
