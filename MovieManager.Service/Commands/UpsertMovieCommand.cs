using Microsoft.EntityFrameworkCore;
using MovieManager.Contract.Responses;
using MovieManager.Data.Context;
using MovieManager.Data.Entites;

namespace MovieManager.Service.Commands
{
    public class UpsertMovieCommand
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Movie UpsertMovie()
        {
            var movie = new Movie
            {
                MovieId = MovieId,
                Title = Title,
                Description = Description,
                ReleaseDate = ReleaseDate
            };

            return movie;
        }
    }

    public class UpsertMovieCommandHandler : IRequestHandler<UpsertMovieCommand, MovieResponse>
    {
        private readonly MovieManagerContext _context;

        public UpsertMovieCommandHandler(MovieManagerContext context)
        {
            _context = context;
        }

        public async Task<MovieResponse> Handle(UpsertMovieCommand request, CancellationToken cancellationToken = default)
        {
            var movie = await GetMovieAsync(request.MovieId, cancellationToken);

            if (movie == null)
            {
                movie = request.UpsertMovie();
                await _context.AddAsync(movie, cancellationToken);
            }

            movie.Title = request.Title;
            movie.Description = request.Description;
            movie.ReleaseDate = request.ReleaseDate;

            await _context.SaveChangesAsync(cancellationToken);

            return new MovieResponse
            {
                MovieId = movie.MovieId,
                Title = request.Title,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate
            };
        }

        private async Task<Movie> GetMovieAsync(int movieId, CancellationToken cancellationToken = default)
        {
            return await _context.Movies.SingleOrDefaultAsync(x => x.MovieId == movieId, cancellationToken);
        }
    }
}
