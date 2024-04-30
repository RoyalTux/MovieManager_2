using Microsoft.EntityFrameworkCore;
using MovieManager.Contract.Responses;
using MovieManager.Data.Context;

namespace MovieManager.Service.Queries
{
    public class GetMoviesQueryHandler : IRequestHandler<IList<MovieResponse>>
    {
        private readonly MovieManagerContext _context;

        public GetMoviesQueryHandler(MovieManagerContext context)
        {
            _context = context;
        }

        public async Task<IList<MovieResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .AsNoTracking()
                .Select(x => new MovieResponse
                {
                    MovieId = x.MovieId,
                    Title = x.Title,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate
                })
                .OrderByDescending(x => x.MovieId)
                .ToListAsync(cancellationToken);
        }
    }
}
