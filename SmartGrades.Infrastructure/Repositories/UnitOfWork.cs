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

        public IRepository<Student> Students { get; }
        public IRepository<Teacher> Teachers { get; }
        public IGradeRepository Grades { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Students = new Repository<Student>(_context);
            Teachers = new Repository<Teacher>(_context);
            Grades = new GradeRepository(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
