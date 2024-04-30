using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManager.Data.Entites
{
    [Table("Session")]
    public class Session
    {
        [Key]
        public int SessionId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        [StringLength(255)]
        public string RoomName { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
