namespace MovieManager.Contract.Responses
{
    public class MovieResponse
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
