using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDI.Models.NatLibMedicine
{
	public class InteractionUserInput
	{
		[JsonPropertyName("sources")]
		public List<string> Sources { get; set; }

		[JsonPropertyName("rxcui")]
		public int DrugId { get; set; }
	}
}