using DDI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDI.DrugApi
{
	public class NlmDrugApi : IDrugApi
	{
		public async Task<List<Interaction>> FetchInteractionsAsync(Drug drug) =>
			new List<Interaction>();
	}
}
