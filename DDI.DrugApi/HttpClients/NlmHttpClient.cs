using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DDI.DrugApi.HttpClients
{
	public class NlmHttpClient
	{
		private const string BaseUrl = "https://rxnav.nlm.nih.gov";

		private readonly HttpClient _httpClient;

		public NlmHttpClient()
		{
			_httpClient = new HttpClient();
		}

		private async Task<T> GetAsync<T>(string path) =>
			await _httpClient.GetFromJsonAsync<T>(path);
	}
}
