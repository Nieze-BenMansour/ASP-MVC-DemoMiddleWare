using DemoMiddleWare.GenericClasses;
using DemoMiddleWare.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace DemoMiddleWare.Controllers
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
            //New
            var bonjourGenericString = new GenericBonjour<string>();
            bonjourGenericString.BonjourAll("Alex");

            var bonjourGenericInt = new GenericBonjour<int>();
            bonjourGenericInt.BonjourAll(20);

            //old
            // Récupère l'information depuis HttpContext.Items
            var customInfo = HttpContext.Items["CustomInfo"] as string;
            ViewBag.CustomInfo = customInfo ?? string.Empty;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
