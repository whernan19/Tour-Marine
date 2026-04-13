using System.Globalization;
using TourMarine.Models;

namespace TourMarine.Services
{
    public class MareaService
    {
        private readonly string _filePath;

        public MareaService()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "mareas.txt");
        }

        public List<Marea> ObtenerMareas()
        {
            var mareas = new List<Marea>();

            if (!File.Exists(_filePath))
                return mareas;

            var lines = File.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                mareas.Add(new Marea
                {
                    Fecha = parts[0],
                    Hora = parts[1],
                    Altura = double.Parse(parts[2], new CultureInfo("es-ES"))
                });
            }

            return mareas;
        }
    }
}
