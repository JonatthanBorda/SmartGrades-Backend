using Microsoft.EntityFrameworkCore;
using SmartGrades.Domain.Entities;

namespace SmartGrades.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuración de relaciones con Fluent API:
            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.Notas)
                .WithOne(n => n.Estudiante)
                .HasForeignKey(n => n.IdEstudiante);

            modelBuilder.Entity<Profesor>()
                .HasMany(p => p.Notas)
                .WithOne(n => n.Profesor)
                .HasForeignKey(n => n.IdProfesor);

            base.OnModelCreating(modelBuilder);
        }
    }
}
