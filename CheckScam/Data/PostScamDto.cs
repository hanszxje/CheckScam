﻿using System.ComponentModel.DataAnnotations;

namespace CheckScam.Data
{
    public class PostScamDto
    {
        [StringLength(50)]
        public string? StkScam { get; set; }

        [StringLength(20)]
        public string? SdtScam { get; set; }

        [Required]
        public string NoiDung { get; set; }
    }
}