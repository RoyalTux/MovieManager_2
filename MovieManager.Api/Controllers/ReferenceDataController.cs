using Microsoft.AspNetCore.Mvc;
using MovieManager.Service.Services;

namespace MovieManager.Api.Controllers
{
	[ApiVersion("1.0")]
	[Route("v{version:apiVersion}/[Controller]")]
	[ApiController]
	public class ReferenceDataController : ControllerBase
	{
		private readonly IRefDataService _refDataService;

		public ReferenceDataController(IRefDataService refDataService)
		{
			_refDataService = refDataService;
		}

		[HttpGet]
		public async Task<IActionResult> GetDirectorsAsync()
		{
			return Ok(await _refDataService.GetData());
		}

		[HttpPost]
		public async Task<IActionResult> PostDirectorsAsync([FromBody] string directorName)
		{
			var test = await _refDataService.PostData(directorName);
			return Ok(test);
		}
	}
}
