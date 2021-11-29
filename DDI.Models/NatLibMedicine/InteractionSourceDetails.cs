using System.Text.Json.Serialization;

namespace DDI.Models.NatLibMedicine
{
	public class InteractionSourceDetails
	{
		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("url")]
		public string Url { get; set; }
	}
}