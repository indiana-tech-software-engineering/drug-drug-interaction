using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DDI.WebApp.Models;

namespace DDI.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public List<Drug> example(string dname){
           // TODO: create api function to replace this one. right now this function creates the list and exports it.
            List<Drug> interacted = new List<Drug>();
            if(dname == "car") {
               for(int i = 1; i<5;i++) {
                  interacted.Add(new Drug{drugName = dname + " " + i, side_effects = "testeffect"});
               }
            }
           return interacted;
        }
        public ActionResult drug_searcher(string drug) {
           //only used to get the ID from the URL. sends it off to a function which does most the job
           // could probably have done it more elegantly but it works
           return View(example(drug));
           //TODO: possibly split into two pages for cleanly-ness (?)
        }
         

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
