using DDI.DrugApi;
using DDI.Models;
using DDI.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DDI.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDrugApi _drugApi;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IDrugApi drugApi)
		{
			_logger = logger;
			_drugApi = drugApi;
		}

		public IActionResult Index() =>
			View();

		public IActionResult Privacy() =>
			View();

		public async Task<IActionResult> Search(string drugName) =>
			View(await _drugApi.FetchInteractions(drugName));

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() =>
			View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
