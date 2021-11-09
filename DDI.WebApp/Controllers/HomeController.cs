using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
           return View(_drugApi.FetchInteractions(drug));
           
        }
         

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
