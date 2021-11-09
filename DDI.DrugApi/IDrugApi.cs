using DDI.Models;
using System.Collections.Generic;

namespace DDI.DrugApi
{
	public interface IDrugApi
	{
		List<Interaction> FetchInteractions(string drugName);
	}
}
