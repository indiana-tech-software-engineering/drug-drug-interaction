using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDI.Models.NatLibMedicine
{
	public class InteractionType
	{
		[JsonPropertyName("comment")]
		public string Comment { get; set; }

		[JsonPropertyName("minConceptItem")]
		public InteractionMinimumDetails MinimumDetails { get; set; }

		[JsonPropertyName("interactionPair")]
		public List<InteractionResult> Interactions { get; set; }
	}
}