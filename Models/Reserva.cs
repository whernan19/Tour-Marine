using System.ComponentModel.DataAnnotations;

namespace TourMarine.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }
        public DateTime Fecha { get; set; }

        public int IdTipoTour { get; set; }
        public string HoraInicio { get; set; } // "07:00"
        public int Duracion { get; set; }      // 3,4,5,7

        public string Nombre { get; set; }
        public int Personas { get; set; }

       

    }
}
