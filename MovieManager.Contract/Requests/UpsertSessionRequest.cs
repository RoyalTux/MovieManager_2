using System.ComponentModel.DataAnnotations;

namespace MovieManager.Contract.Requests
{
    public class UpsertSessionRequest
    {
        public int SessionId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        [StringLength(255)]
        public string RoomName { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }
    }
}
