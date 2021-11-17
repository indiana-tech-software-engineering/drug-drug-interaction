using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DDI.Models;
using DDI.WebApp.Models;
using DDI.DrugApi;

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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
		
        public ActionResult drug_searcher(string drug) {
			if(drug != null) {
				return View(_drugApi.FetchInteractions(drug));
			} return View(new List<Interaction>());
        }
         

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
