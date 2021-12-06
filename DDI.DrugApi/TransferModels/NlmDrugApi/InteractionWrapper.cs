using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDI.DrugApi.TransferModels.NlmDrugApi
{
	public class InteractionWrapper
	{
		[JsonPropertyName("nlmDisclaimer")]
		public string Disclaimer { get; set; }

		[JsonPropertyName("userInput")]
		public InteractionUserInput UserInput { get; set; }

		[JsonPropertyName("interactionTypeGroup")]
		public List<InteractionTypeGroup> TypeGroups { get; set; }
	}
}
