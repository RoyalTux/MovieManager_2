using Microsoft.EntityFrameworkCore;
using MovieManager.Contract.Responses;
using MovieManager.Data.Context;

namespace MovieManager.Service.Queries
{
    public class GetSessionsQueryHandler : IRequestHandler<IList<SessionResponse>>
    {
        private readonly MovieManagerContext _context;

        public GetSessionsQueryHandler(MovieManagerContext context)
        {
            _context = context;
        }

        public async Task<IList<SessionResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Sessions
                .AsNoTracking()
                .Include(x => x.Movie)
                .Select(x => new SessionResponse
                {
                    SessionId = x.SessionId,
                    MovieId = x.MovieId,
                    RoomName = x.RoomName,
                    StartDateTime = x.StartDateTime,
                    MovieResponse = new MovieResponse
                    {
                        MovieId = x.Movie.MovieId,
                        Title = x.Movie.Title,
                        Description = x.Movie.Description,
                        ReleaseDate = x.Movie.ReleaseDate
                    }
                })
                .OrderByDescending(x => x.SessionId)
                .ToListAsync(cancellationToken);
        }
    }
}
