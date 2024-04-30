using Microsoft.Extensions.Configuration;
using MovieManager.Service.Common;
using Newtonsoft.Json;

namespace MovieManager.Service.Services
{
	public interface IRefDataService
	{
		Task<IEnumerable<string>> GetData();
		Task<IEnumerable<string>> PostData(string dirName);
	}

	public class RefDataService : HttpClientBase, IRefDataService
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;

		public RefDataService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
		{
			_httpClient = httpClientFactory.CreateClient();
			_configuration = configuration;
		}

		public async Task<IEnumerable<string>> GetData()
		{
			var endpoint = _configuration.GetSection("RefDataEndpoints:Directors").Value;
			return (await Get<List<string>>(_httpClient, new Uri(endpoint))).Value;
		}

		public async Task<IEnumerable<string>> PostData(string dirName)
		{
			var endpoint = _configuration.GetSection("RefDataEndpoints:Directors").Value;
			var body = JsonConvert.SerializeObject(dirName);
			return (await Post<List<string>>(_httpClient, new Uri(endpoint), body)).Value;
		}
	}
}
