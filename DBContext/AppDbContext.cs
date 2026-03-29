namespace TourMarine.DBContext

{
    using Microsoft.EntityFrameworkCore;
    using TourMarine.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<TipoTour> TipoTour { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Si quieres evitar relaciones automáticas, configura solo las claves
            modelBuilder.Entity<Reserva>()
                .HasKey(r => r.IdReserva);
        }

    }
}
