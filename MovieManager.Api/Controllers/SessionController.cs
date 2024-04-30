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
    public class SessionController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSessionAsync([FromServices] IRequestHandler<IList<SessionResponse>> getSessionsQuery)
        {
            return Ok(await getSessionsQuery.Handle());
        }

        [HttpPost]
        public async Task<IActionResult> UpsertSessionAsync([FromServices] IRequestHandler<UpsertSessionCommand, SessionResponse> upsertSessionCommand, [FromBody] UpsertSessionRequest request)
        {
            var session = await upsertSessionCommand.Handle(new UpsertSessionCommand
            {
                SessionId = request.SessionId,
                MovieId = request.MovieId,
                RoomName = request.RoomName,
                StartDateTime = request.StartDateTime
            });

            return Ok(session);
        }
    }
}
