using Polly;
using System.Text;

namespace MovieManager.Service.Common
{
    public abstract class HttpClientBase
    {
        public static async Task<RequestResult<T>> Post<T>(HttpClient httpClient, Uri uri, string content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };

            const int maxRetryAttempts = 5;
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(maxRetryAttempts, i => TimeSpan.FromSeconds(i * 5));

            var result = new RequestResult<T>();
            try
            {
                await retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    result.Value = await response.Content.ReadAsAsync<T>();
                });

                result.Stop();
                return result;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Stop();
                return result;
            }
        }

		public static async Task<RequestResult<T>> Get<T>(HttpClient httpClient, Uri uri)
		{
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

			const int maxRetryAttempts = 5;
			var retryPolicy = Policy
				.Handle<HttpRequestException>()
				.WaitAndRetryAsync(maxRetryAttempts, i => TimeSpan.FromSeconds(i * 5));

			var result = new RequestResult<T>();
			try
			{
				await retryPolicy.ExecuteAsync(async () =>
				{
					var response = await httpClient.SendAsync(request);
					response.EnsureSuccessStatusCode();
					result.Value = await response.Content.ReadAsAsync<T>();
				});

				result.Stop();
				return result;
			}
			catch (Exception ex)
			{
				result.Exception = ex;
				result.Stop();
				return result;
			}
		}
	}
}
