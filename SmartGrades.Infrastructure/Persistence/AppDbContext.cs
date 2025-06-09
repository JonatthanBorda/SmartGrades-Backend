using Microsoft.EntityFrameworkCore;
using SmartGrades.Domain.Entities;

namespace SmartGrades.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Estudiantes { get; set; }
        public DbSet<Teacher> Profesores { get; set; }
        public DbSet<Grade> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuración de relaciones con Fluent API:
            modelBuilder.Entity<Student>()
                .HasMany(e => e.Grades)
                .WithOne(n => n.Student)
                .HasForeignKey(n => n.IdStudent);

            modelBuilder.Entity<Teacher>()
                .HasMany(p => p.Grades)
                .WithOne(n => n.Teacher)
                .HasForeignKey(n => n.IdTeacher);

            base.OnModelCreating(modelBuilder);
        }
    }
}
