using System.Text.Json.Serialization;

namespace DDI.Models.NatLibMedicine
{
	public class InteractionDrug
	{
		[JsonPropertyName("minConceptItem")]
		public InteractionMinimumDetails MinimumDetails { get; set; }

		[JsonPropertyName("sourceConceptItem")]
		public InteractionSourceDetails SourceDetails { get; set; }
	}
}