using AutoFixture;
using MovieManager.Contract.Responses;
using MovieManager.Data.Context;
using MovieManager.Data.Entites;
using MovieManager.Service.Commands;

namespace MovieManager.Service.Tests.Commands
{
    public class UpsertMovieCommandTests
    {
        private readonly DbContextDecorator<MovieManagerContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public UpsertMovieCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<MovieManagerContext>();

            _dbContext = new DbContextDecorator<MovieManagerContext>(options);
        }

        [SetUp]
        public void Init()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.MovieId, 1).Create();
            _dbContext.AddAndSave(movie);
        }

        [TearDown]
        public void Cleanup()
        {
            _dbContext.Clear();
        }

        [Test]
        public void AddsMovie()
        {
            var fixture = new Fixture();

            var movieCommand = fixture.Build<UpsertMovieCommand>()
                .With(x => x.MovieId, 2)
                .Create();

            _dbContext.Assert(async context =>
            {
                // Arrange
                var sut = CreateSut(context);

                // Act
                var result = await sut.Handle(movieCommand, _cts.Token);

                // Assert
                Assert.That(result, Is.Not.Null);
                AssertMovieResult(movieCommand, result);
            });
        }

        [Test]
        public void UpdatesMovie()
        {
            var fixture = new Fixture();

            var movieCommand = fixture.Build<UpsertMovieCommand>()
                .With(x => x.MovieId, 1)
                .Create();

            _dbContext.Assert(async context =>
            {
                // Arrange
                var sut = CreateSut(context);

                // Act
                var result = await sut.Handle(movieCommand, _cts.Token);

                // Assert
                Assert.That(result, Is.Not.Null);
                AssertMovieResult(movieCommand, result);
            });
        }

        private static void AssertMovieResult(UpsertMovieCommand expected, MovieResponse result)
        {
            Assert.Multiple(() =>
            {
                Assert.That(result.MovieId, Is.EqualTo(expected.MovieId));
                Assert.That(result.Title, Is.EqualTo(expected.Title));
                Assert.That(result.Description, Is.EqualTo(expected.Description));
                Assert.That(result.ReleaseDate, Is.EqualTo(expected.ReleaseDate));
            });
        }

        private static UpsertMovieCommandHandler CreateSut(MovieManagerContext context) => new(context);
    }
}
