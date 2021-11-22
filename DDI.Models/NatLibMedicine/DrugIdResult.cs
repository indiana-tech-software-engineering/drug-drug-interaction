using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDI.Models.NatLibMedicine
{
	public class DrugIdResult
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("rxnormId")]
		public List<int> DrugId { get; set; }
	}
}
