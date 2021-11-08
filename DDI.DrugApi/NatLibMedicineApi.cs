using DDI.Models;
using System.Collections;

namespace DDI.DrugApi
{
	public class NatLibMedicineApi : IDrugApi
	{
		public List<Interaction> FetchInteractions(Drug drug)
		{
			return new List<Interaction>();
		}
	}
}
