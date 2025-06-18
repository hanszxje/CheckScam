using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CheckScam.Models
{
    public class ScamPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NameScam { get; set; }

        [StringLength(50)]
        public string? StkScam { get; set; }

        [StringLength(20)]
        public string? SdtScam { get; set; }

        [Required]
        public string NoiDung { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<ScamImage> Images { get; set; } = new List<ScamImage>();
    }
}