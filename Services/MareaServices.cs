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
            var list = new List<Marea>();

            if (!File.Exists(_filePath))
                return list;

            var lines = File.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 3)
                    continue;

                string fechaTexto = parts[0] + " " + parts[1];

                DateTime fecha = DateTime.ParseExact(
                    fechaTexto,
                    "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture
                );

                double altura = double.Parse(parts[2], new CultureInfo("es-ES"));

                list.Add(new Marea
                {
                    Fecha = fecha,
                    Hora = parts[1],
                    Altura = altura
                });
            }

            return list;
        }

    }

}
