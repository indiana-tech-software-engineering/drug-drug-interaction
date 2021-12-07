using System.Text.Json.Serialization;

namespace DDI.DrugApi.TransferModels.NlmDrugApi
{
	public class InteractionMinimumDetails
	{
		[JsonPropertyName("rxcui")]
		public int DrugId { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("tty")]
		public string Tty { get; set; }
	}
}
