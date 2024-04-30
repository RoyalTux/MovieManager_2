using AutoFixture;
using MovieManager.Data.Context;
using MovieManager.Data.Entites;
using MovieManager.Service.Queries;

namespace MovieManager.Service.Tests.Queries
{
    public class GetMoviesQueryTests
    {
        private readonly DbContextDecorator<MovieManagerContext> _dbContext;

        public GetMoviesQueryTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<MovieManagerContext>();

            _dbContext = new DbContextDecorator<MovieManagerContext>(options);
        }

        //[Test]
        //public void DataSet_ReturnsCorrectRows()
        //{
        //    var fixture = new Fixture();
        //    var movie1 = fixture.Build<Movie>().With(x => x.MovieId, 1).Create();
        //    var movie2 = fixture.Build<Movie>().With(x => x.MovieId, 2).Create();
        //    _dbContext.AddAndSaveRange(new List<Movie> { movie1, movie2 });

        //    _dbContext.Assert(async context => {
        //        // Arrange
        //        var sut = CreateSut(context);

        //        // Act
        //        var results = await sut.Handle();

        //        // Assert
        //        Assert.That(results, Is.Not.Null);
        //        Assert.That(results.Count, Is.EqualTo(2));

        //        var firstMovie = results.Last();
        //        Assert.Multiple(() =>
        //        {
        //            Assert.That(firstMovie.MovieId, Is.EqualTo(movie1.MovieId));
        //            Assert.That(firstMovie.Title, Is.EqualTo(movie1.Title));
        //            Assert.That(firstMovie.Description, Is.EqualTo(movie1.Description));
        //            Assert.That(firstMovie.ReleaseDate, Is.EqualTo(movie1.ReleaseDate));
        //        });
        //    });
        //}

		[Test]
		public void GetMovieById_ReturnsCorrectRow()
		{
			var fixture = new Fixture();
			var movie1 = fixture.Build<Movie>().With(x => x.MovieId, 1).Create();
			var movie2 = fixture.Build<Movie>().With(x => x.MovieId, 2).Create();
			_dbContext.AddAndSaveRange(new List<Movie> { movie1, movie2 });

			_dbContext.Assert(async context => {
				// Arrange
				var sut = CreateSut(context);
                var expectedMovieId = 1;

				// Act
				var firstMovie = await sut.Handle(expectedMovieId);

				// Assert
				Assert.That(firstMovie, Is.Not.Null);

				Assert.Multiple(() =>
				{
					Assert.That(firstMovie.MovieId, Is.EqualTo(movie1.MovieId));
					Assert.That(firstMovie.Title, Is.EqualTo(movie1.Title));
					Assert.That(firstMovie.Description, Is.EqualTo(movie1.Description));
					Assert.That(firstMovie.ReleaseDate, Is.EqualTo(movie1.ReleaseDate));
				});
			});
		}

		private static GetMovieByIdQueryHandler CreateSut(MovieManagerContext context) => new(context);
    }
}
