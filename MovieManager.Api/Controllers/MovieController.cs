using Microsoft.AspNetCore.Mvc;
using MovieManager.Contract.Requests;
using MovieManager.Contract.Responses;
using MovieManager.Service;
using MovieManager.Service.Commands;

namespace MovieManager.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMoviesAsync([FromServices] IRequestHandler<IList<MovieResponse>> getMoviesQuery)
        {
            return Ok(await getMoviesQuery.Handle());
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovieByIdAsync(int movieId, [FromServices] IRequestHandler<int, MovieResponse> getMovieByIdQuery)
        {
            return Ok(await getMovieByIdQuery.Handle(movieId));
        }

        [HttpPost]
        public async Task<IActionResult> UpserMovieAsync([FromServices] IRequestHandler<UpsertMovieCommand, MovieResponse> upsertMovieCommand, [FromBody] UpsertMovieRequest request)
        {
            var movie = await upsertMovieCommand.Handle(new UpsertMovieCommand
            {
                MovieId = request.MovieId,
                Title = request.Title,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate
            });

            return Ok(movie);
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovieById(int movieId, [FromServices] IRequestHandler<DeleteMovieCommand, bool> deleteMovieByIdCommand)
        {
            var result = await deleteMovieByIdCommand.Handle(new DeleteMovieCommand { MovieId = movieId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
