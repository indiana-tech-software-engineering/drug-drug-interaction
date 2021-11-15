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
			_drugClient.doTheThing(drugName);
			return new List<Interaction>
			
			{
				new Interaction
				{
					Drug = new Drug
					{
						CommonName = "Tylenol",
						ScientificName = "aceteminophin",
					},
					Description = "This does things.",
				},
				new Interaction
				{
					Drug = new Drug
					{
						CommonName = "Orange",
						ScientificName = "Juice",
					},
					Description = "corn.",
				}
			};
		}
	}
}
