using DDI.DrugApi.HttpClients;
using DDI.DrugApi.TransferModels.NlmDrugApi;
using DDI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDI.DrugApi.Apis
{
	public class NlmDrugApi : IDrugApi
	{
		private readonly NlmHttpClient _nlmHttpClient;

		public NlmDrugApi(NlmHttpClient nlmHttpClient)
		{
			_nlmHttpClient = nlmHttpClient;
		}

		public async Task<bool> FetchIsDrugValidByDrugNameAsync(string drugName) =>
			await FetchDrugIdByName(drugName) != null;

		public async Task<IEnumerable<Interaction>> FetchDrugInteractionsByDrugNameAsync(string drugName)
		{
			if (string.IsNullOrEmpty(drugName))
				return new List<Interaction>();

			var drugId = await FetchDrugIdByName(drugName);
			var interactionResults = await _nlmHttpClient.FetchDrugInteractionsByDrugId(drugId);

			return interactionResults?.Select(AsInteractions) ?? new List<Interaction>();
		}

		private async Task<int?> FetchDrugIdByName(string drugName)
		{
			var result = await _nlmHttpClient.FetchDrugIdByName(drugName);

			return (result?.DrugId != null && 1 <= result?.DrugId?.Count)
				? result.DrugId[0]
				: null;
		}

		private Interaction AsInteractions(InteractionResult result) => new Interaction
		{
			Description = result.Description,
			Drug = new Drug
			{
				Id = result.Drugs[1].MinimumDetails.DrugId,
				Name = result.Drugs[1].MinimumDetails.Name,
			}
		};
	}
}
