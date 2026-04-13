using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TourMarine.Models;
using TourMarine.Services;

namespace TourMarine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MareaService _mareaService; //ingresmos al marea services

        public HomeController(ILogger<HomeController> logger, MareaService mareaService)
        {
            _logger = logger;
            _mareaService = mareaService;
        }

        public IActionResult Index()
        {
            var mareas = _mareaService.ObtenerMareas();
            return View(mareas);
            
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
