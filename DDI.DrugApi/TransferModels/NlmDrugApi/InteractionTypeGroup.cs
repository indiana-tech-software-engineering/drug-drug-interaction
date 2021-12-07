using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDI.DrugApi.TransferModels.NlmDrugApi
{
	public class InteractionTypeGroup
	{
		[JsonPropertyName("sourceDisclaimer")]
		public string SourceDisclaimer { get; set; }

		[JsonPropertyName("sourceName")]
		public string SourceName { get; set; }

		[JsonPropertyName("interactionType")]
		public List<InteractionType> Types { get; set; }
	}
}
