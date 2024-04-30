using Microsoft.EntityFrameworkCore;
using MovieManager.Contract.Responses;
using MovieManager.Data.Context;
using MovieManager.Data.Entites;

namespace MovieManager.Service.Commands
{
    public class UpsertSessionCommand
    {
        public int SessionId { get; set; }

        public int MovieId { get; set; }

        public string RoomName { get; set; }

        public DateTime StartDateTime { get; set; }

        public Session UpsertSession()
        {
            var session = new Session
            {
                SessionId = SessionId,
                MovieId = MovieId,
                RoomName = RoomName,
                StartDateTime = StartDateTime
            };

            return session;
        }
    }

    public class UpsertSessionCommandHandler : IRequestHandler<UpsertSessionCommand, SessionResponse>
    {
        private readonly MovieManagerContext _context;

        public UpsertSessionCommandHandler(MovieManagerContext context)
        {
            _context = context;
        }

        public async Task<SessionResponse> Handle(UpsertSessionCommand request, CancellationToken cancellationToken = default)
        {
            var session = await GetSessionAsync(request.SessionId, cancellationToken);

            if (session == null)
            {
                session = request.UpsertSession();
                await _context.AddAsync(session, cancellationToken);
            }

            session.MovieId = request.MovieId;
            session.RoomName = request.RoomName;
            session.StartDateTime = request.StartDateTime;

            await _context.SaveChangesAsync(cancellationToken);

            return new SessionResponse
            {
                SessionId = session.SessionId,
                MovieId = session.MovieId,
                RoomName = session.RoomName,
                StartDateTime = session.StartDateTime,
                MovieResponse = session?.Movie != null ? new MovieResponse
                {
                    MovieId = session.Movie.MovieId,
                    Title = session.Movie.Title,
                    Description = session.Movie.Description,
                    ReleaseDate = session.Movie.ReleaseDate
                } : null
            };
        }

        private async Task<Session> GetSessionAsync(int sessionId, CancellationToken cancellationToken = default)
        {
            return await _context.Sessions.Include(x => x.Movie).SingleOrDefaultAsync(x => x.SessionId == sessionId, cancellationToken);
        }
    }
}
