using Microsoft.EntityFrameworkCore;
using MovieManager.Contract.Responses;
using MovieManager.Data.Context;

namespace MovieManager.Service.Queries
{
    public class GetMovieByIdQueryHandler : IRequestHandler<int, MovieResponse>
    {
        private readonly MovieManagerContext _context;

        public GetMovieByIdQueryHandler(MovieManagerContext context)
        {
            _context = context;
        }

        public async Task<MovieResponse> Handle(int movieId, CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .AsNoTracking()
                .Where(x => x.MovieId == movieId)
                .Select(x => new MovieResponse
                {
                    MovieId = x.MovieId,
                    Title = x.Title,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
