using Microsoft.EntityFrameworkCore;
using MovieManager.Data.Entites;

namespace MovieManager.Data.Context
{
    public class MovieManagerContext : DbContext
    {
        public MovieManagerContext(DbContextOptions<MovieManagerContext> options) : base(options) { }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
