using System.ComponentModel.DataAnnotations;

namespace CheckScam.Data
{
    public class PostScamUrlDto
    {
        [Required]
        [StringLength(500)]
        public string Url { get; set; }

        [Required]
        public string NoiDung { get; set; }
    }
}