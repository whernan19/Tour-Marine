using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Net.Mail;
using TourMarine.DBContext;
using TourMarine.Models;
namespace TourMarine.Controllers
{


    public class ReservacionesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public ReservacionesController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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

    
        public IActionResult ProbarCorreo()
        {
            try
            {
                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("asdfasdf", "asdfasdfasdf"),
                    EnableSsl = true
                };

                var mail = new MailMessage();
                mail.From = new MailAddress("TU_CORREO@gmail.com");
                mail.To.Add("w.hernan.ramirez.19@gmail.com");

                mail.Subject = "PRUEBA TOUR MARINE";
                mail.Body = "Este es un correo de prueba 🔥";

                smtp.Send(mail);

                return Content("Correo enviado correctamente");
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }
    }
}
