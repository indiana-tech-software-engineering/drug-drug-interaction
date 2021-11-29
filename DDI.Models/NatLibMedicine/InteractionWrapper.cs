using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDI.Models.NatLibMedicine
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