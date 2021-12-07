using DDI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDI.DrugApi.Apis
{
	public interface IDrugApi
	{
		Task<bool> FetchIsDrugValidByDrugNameAsync(string drugName);
		Task<IEnumerable<Interaction>> FetchDrugInteractionsByDrugNameAsync(string drugName);
	}
}
