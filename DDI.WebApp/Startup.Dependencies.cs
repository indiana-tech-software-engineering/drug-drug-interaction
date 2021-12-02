using DDI.DrugApi;
using Microsoft.Extensions.DependencyInjection;

namespace DDI.WebApp
{
	public partial class Startup
	{
		public void InjectDependencies(IServiceCollection services)
		{
			services.AddScoped<IDrugApi, NlmDrugApi>();
		}
	}
}
