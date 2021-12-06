using System.Net.Http;
using DDI.DrugApi.Apis;
using DDI.DrugApi.HttpClients;
using Microsoft.Extensions.DependencyInjection;

namespace DDI.WebApp
{
	public partial class Startup
	{
		public void InjectDependencies(IServiceCollection services)
		{
			services.AddSingleton<NlmHttpClient>();
			services.AddScoped<IDrugApi, NlmDrugApi>();
		}
	}
}
