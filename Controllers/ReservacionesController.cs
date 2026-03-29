using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourMarine.DBContext;
using TourMarine.Models;
using System.Linq;
namespace TourMarine.Controllers
{
   

    public class ReservacionesController : Controller
    {
        private readonly AppDbContext _context;
        public ReservacionesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: ReservacionesController
        public IActionResult Index()
        {
            ViewBag.Tours = _context.TipoTour.ToList();
            return View();
        }

        // GET: ReservacionesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservacionesController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearReserva(Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                // opcional: devolver la misma vista con errores
                return View(reserva);
            }

            _context.Reserva.Add(reserva);
            _context.SaveChanges();

            // Redirige al Home
            return RedirectToAction("Index", "Home");
        }

        // GET: ReservacionesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservacionesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservacionesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservacionesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // 🔥 ESTE ES EL IMPORTANTE
        [HttpGet]
        public IActionResult ObtenerReservasPorFecha(string fecha)
        {
            DateTime fechaConvertida = DateTime.Parse(fecha);

            var reservas = _context.Reserva
                .Where(r => r.Fecha.Date == fechaConvertida.Date)
                .Select(r => new
                {
                    horaInicio = r.HoraInicio,
                    duracion = r.Duracion
                })
                .ToList();

            return Json(reservas);
        }
        //precio por personas y horas 
        public decimal CalcularPrecio(string duracion, int personas)
        {
            int basePersonas = 6;
            decimal precioBase = duracion switch
            {
                "3" => 600m,
                "4" => 700m,
                "5" => 800m,
                "7" => 900m,
                "full" => 1200m,
                _ => 0m
            };

            int extraPersonas = Math.Max(personas - basePersonas, 0);
            decimal precioFinal = precioBase + (extraPersonas * 50m);

            return precioFinal;
        }

    }
}
