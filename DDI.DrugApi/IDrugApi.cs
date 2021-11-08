using DDI.Models;
using System.Collections;

namespace DDI.DrugApi
{
	public interface IDrugApi
	{
		List<Interaction> FetchInteractions(Drug drug);
	}
}
