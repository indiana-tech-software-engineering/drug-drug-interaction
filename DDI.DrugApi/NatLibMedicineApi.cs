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
			var fresult = await _natLibMedicineHttpClient.FetchDrugInteraction(result.DrugId[0]);
			
			if (fresult.TypeGroups == null)
				return new List<Interaction>();

			if (result?.DrugId == null || result.DrugId.Count == 0)
				return new List<Interaction>();
		
			var list = new List<Interaction>();
			foreach (var item in fresult.TypeGroups[0].Types[0].Interactions)
			{
				list.Add
				(
					new Interaction
					{
						Drug = new Drug
						{
							Id = item.Drugs[1].MinimumDetails.DrugId,
							ScientificName = item.Drugs[1].SourceDetails.Name,
							CommonName = $"Common {item.Drugs[1].SourceDetails.Name}",
						},
						Description = item.Description
					}
				);
			}
			return list;
		}
	}
}
