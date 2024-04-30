using Microsoft.EntityFrameworkCore;
using MovieManager.Data.Context;
using MovieManager.Data.Entites;

namespace MovieManager.Service.Commands
{
    public class DeleteMovieCommand
    {
        public int MovieId { get; set; }
    }

    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, bool>
    {
        private readonly MovieManagerContext _context;

        public DeleteMovieCommandHandler(MovieManagerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken = default)
        {
            var movie = await GetMovieAsync(request.MovieId, cancellationToken);

            if (movie != null)
            {
                _context.Remove(movie);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Movie> GetMovieAsync(int movieId, CancellationToken cancellationToken = default)
        {
            return await _context.Movies.SingleOrDefaultAsync(x => x.MovieId == movieId, cancellationToken);
        }
    }
}
