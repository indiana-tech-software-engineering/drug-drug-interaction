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
		private readonly NatLibMedicineHttpClient _natLibMedicineHttpClient;

		public NatLibMedicineApi(NatLibMedicineHttpClient natLibMedicineHttpClient)
		{
			_natLibMedicineHttpClient = natLibMedicineHttpClient;
		}

		public async Task<List<Interaction>> FetchInteractions(string drugName)
		{
			if (drugName == null)
				return new List<Interaction>();

			var result = await _natLibMedicineHttpClient.FetchDrugIdByName(drugName);

			if (result?.DrugId == null || result.DrugId.Count == 0)
				return new List<Interaction>();

			return new List<Interaction>
			{
				new Interaction
				{
					Drug = new Drug
					{
						Id = result.DrugId[0],
						ScientificName = drugName,
						CommonName = $"Common {drugName}",
					}
				}
			};
		}
	}
}
