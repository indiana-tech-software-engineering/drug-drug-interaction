using DDI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDI.DrugApi
{
	public interface IDrugApi
	{
		Task<List<Interaction>> FetchInteractionsAsync(Drug drug);
	}
}
