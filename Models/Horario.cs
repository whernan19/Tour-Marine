using System.ComponentModel.DataAnnotations;

namespace TourMarine.Models
{
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }
        public string Nombre { get; set; }

    }
}