using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManager.Data.Entites
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
