namespace MovieManager.Contract.Responses
{
    public class SessionResponse
    {
        public int SessionId { get; set; }

        public int MovieId { get; set; }

        public string RoomName { get; set; }

        public DateTime StartDateTime { get; set; }

        public virtual MovieResponse MovieResponse { get; set; }
    }
}
