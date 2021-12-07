using System.Text.Json.Serialization;

namespace DDI.DrugApi.TransferModels.NlmDrugApi
{
	public class DrugIdResultWrapper
	{
		[JsonPropertyName("idGroup")]
		public DrugIdResult Result { get; set; }
	}
}
