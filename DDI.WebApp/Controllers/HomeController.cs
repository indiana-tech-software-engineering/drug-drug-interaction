using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DDI.WebApp.Models;
using DDI.DrugApi.Apis;
using System.Threading.Tasks;

namespace DDI.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDrugApi _drugApi;

		public HomeController(
			IDrugApi drugApi
		)
		{
			_drugApi = drugApi;
		}

		public async Task<IActionResult> Index(string drugName) => View((
			await _drugApi.FetchIsDrugValidByDrugNameAsync(drugName),
			await _drugApi.FetchDrugInteractionsByDrugNameAsync(drugName)
		));

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() =>
			View(GetErrorDetails());

		private ErrorViewModel GetErrorDetails() => new ErrorViewModel
		{
			RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
		};
	}
}
