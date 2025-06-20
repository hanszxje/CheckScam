using System.ComponentModel.DataAnnotations;

namespace CheckScam.Models
{
    public class ScamUrl
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Url { get; set; }

        [Required]
        public string NoiDung { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}