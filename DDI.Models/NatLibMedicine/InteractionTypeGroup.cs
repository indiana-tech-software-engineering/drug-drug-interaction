using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDI.Models.NatLibMedicine
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