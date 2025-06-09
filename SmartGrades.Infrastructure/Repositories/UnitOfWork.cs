using SmartGrades.Domain.Entities;
using SmartGrades.Domain.Interfaces;
using SmartGrades.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<Estudiante> Estudiantes { get; }
        public IRepository<Profesor> Profesores { get; }
        public IRepository<Nota> Notas { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Estudiantes = new Repository<Estudiante>(_context);
            Profesores = new Repository<Profesor>(_context);
            Notas = new Repository<Nota>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
