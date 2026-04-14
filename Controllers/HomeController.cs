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

        public IActionResult Index(int? dia, int? mes)
        {
            DateTime hoy = DateTime.Now;

            var mareas = _mareaService.ObtenerMareas()
                .Where(x => x.Fecha >= hoy)
                .OrderBy(x => x.Fecha)
                .Take(10)
                .ToList();

            return View(mareas);
        }

        public IActionResult Filtrar(int? dia, int? mes)
        {
            var mareas = _mareaService.ObtenerMareas();

            if (dia.HasValue)
                mareas = mareas.Where(x => x.Fecha.Day == dia.Value).ToList();

            if (mes.HasValue)
                mareas = mareas.Where(x => x.Fecha.Month == mes.Value).ToList();

            mareas = mareas
                .OrderBy(x => x.Fecha)
                .ToList();

            return View("Index", mareas);
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
        [HttpGet]
        public IActionResult FiltrarAjax(int? dia, int? mes)
        {
            var data = _mareaService.ObtenerMareas();

            if (dia.HasValue)
                data = data.Where(x => x.Fecha.Day == dia).ToList();

            if (mes.HasValue)
                data = data.Where(x => x.Fecha.Month == mes).ToList();

            return PartialView("_TablaMareas", data);
        }
    }
}
