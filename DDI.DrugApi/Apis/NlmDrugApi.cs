using DDI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDI.DrugApi.Apis
{
	public class NlmDrugApi : IDrugApi
	{
		public async Task<List<Interaction>> FetchDrugInteractionsByDrugNameAsync(string drugName)
		{
			if (string.IsNullOrEmpty(drugName))
				return new List<Interaction>();

			throw new NotImplementedException("");
		}
	}
}
