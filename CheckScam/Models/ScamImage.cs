using System.ComponentModel.DataAnnotations;

namespace CheckScam.Models
{
    public class ScamImage
    {
        public int Id { get; set; }

        [Required]
        public int ScamPostId { get; set; }

        public ScamPost ScamPost { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}