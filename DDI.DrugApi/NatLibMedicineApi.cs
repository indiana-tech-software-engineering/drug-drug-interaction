using DDI.Models;
using System.Collections.Generic;

namespace DDI.DrugApi
{
	public class NatLibMedicineApi : IDrugApi
	{
		public List<Interaction> FetchInteractions(string drugName)
		{
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
            }, new Interaction
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
