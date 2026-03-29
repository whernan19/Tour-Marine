using System.ComponentModel.DataAnnotations;

namespace TourMarine.Models
{
    public class TipoTour
    {
        [Key]
        public int IdTipoTour { get; set; }
        public string Nombre { get; set; }

    }
}