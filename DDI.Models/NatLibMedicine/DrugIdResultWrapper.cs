using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDI.Models.NatLibMedicine
{
	public class DrugIdResultWrapper
	{
		[JsonPropertyName("idGroup")]
		public DrugIdResult Result { get; set; }
	}
}
