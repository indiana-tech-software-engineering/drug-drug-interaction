using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DDI.DrugApi.TransferModels.NlmDrugApi;

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

		public virtual async Task<DrugIdResult> FetchDrugIdByName(string drugName) =>
			drugName != null
				? (await GetAsync<DrugIdResultWrapper>($"/REST/rxcui.json?name={drugName}")).Result
				: null;

		public virtual async Task<List<InteractionResult>> FetchDrugInteractionsByDrugId(int? drugId) =>
			drugId != null
				? (await GetAsync<InteractionWrapper>($"/REST/interaction/interaction.json?rxcui={drugId}&sources=ONCHigh"))
					.TypeGroups?[0]?.Types?[0]?.Interactions
				: null;

		private async Task<T> GetAsync<T>(string path) =>
			await _httpClient.GetFromJsonAsync<T>($"{BaseUrl}{path}");
	}
}
