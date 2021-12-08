using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DDI.DrugApi.HttpClients
{
	public class DefaultHttpClient
	{
		private readonly HttpClient _httpClient;

		public DefaultHttpClient()
		{
			_httpClient = new HttpClient();
		}

		public virtual async Task<T> GetFromJsonAsync<T>(string requestUri) =>
			await _httpClient.GetFromJsonAsync<T>(requestUri);
	}
}
