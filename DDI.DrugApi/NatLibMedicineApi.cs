using DDI.Models;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DDI.DrugApi
{
	public class NatLibMedicineApi : IDrugApi
	{
		private readonly NatLibMedicineHttpClient _drugClient;

		public NatLibMedicineApi(NatLibMedicineHttpClient drugClient) {
			_drugClient = drugClient;
		}
		public List<Interaction> FetchInteractions(string drugName)
		{
			return new List<Interaction>
			
			{
				new Interaction
				{
					Drug = new Drug
					{
						CommonName = "Tylenol",
						rxnormId = _drugClient.fetchDrugID(drugName),
						ScientificName = drugName,
					},
					Description = "This does things.",
				},
				new Interaction
				{
					Drug = new Drug
					{
						CommonName = "Orange",
						rxnormId = "2314124",
						ScientificName = "Juice",
					},
					Description = "corn.",
				}
			};
		}
	}
}
