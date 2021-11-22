using DDI.Models.NatLibMedicine;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DDI.DrugApi
{
	public class NatLibMedicineHttpClient
	{
		private const string BaseUri = "https://rxnav.nlm.nih.gov";

		private readonly HttpClient _httpClient;

		public NatLibMedicineHttpClient()
		{
			_httpClient = new HttpClient();
		}

		public async Task<DrugIdResult> FetchDrugIdByName(string drugName) =>
			(await GetAsync<DrugIdResultWrapper>($"/REST/rxcui.json?name={drugName}")).Result;

		private async Task<T> GetAsync<T>(string path) =>
			await _httpClient.GetFromJsonAsync<T>($"{BaseUri}{path}");
	}
}
