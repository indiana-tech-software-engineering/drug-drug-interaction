using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDI.DrugApi.TransferModels.NlmDrugApi
{
	public class InteractionUserInput
	{
		[JsonPropertyName("sources")]
		public List<string> Sources { get; set; }

		[JsonPropertyName("rxcui")]
		public int DrugId { get; set; }
	}
}
