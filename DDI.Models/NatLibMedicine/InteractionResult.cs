using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDI.Models.NatLibMedicine
{
	public class InteractionResult
	{
		[JsonPropertyName("interactionConcept")]
		public List<InteractionDrug> Drugs { get; set; }

		[JsonPropertyName("severity")]
		public string Severity { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }
	}
}