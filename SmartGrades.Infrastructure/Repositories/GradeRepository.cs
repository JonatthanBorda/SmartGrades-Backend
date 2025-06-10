using Microsoft.EntityFrameworkCore;
using SmartGrades.Domain.Entities;
using SmartGrades.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Infrastructure.Repositories
{
    internal class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(DbContext context) : base(context) { }

        public async Task<IEnumerable<Grade>> GetAllWithTeacherAndStudentAsync()
        {
            return await _entities
                .Include(g => g.Teacher)
                .Include(g => g.Student)
                .ToListAsync();
        }

        public async Task<Grade?> GetByIdWithTeacherAndStudentAsync(int id)
        {
            return await _entities
                .Include(g => g.Teacher)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Grade>> FindAsyncWithTeacherAndStudentAsync(Expression<Func<Grade, bool>> predicate)
        {
            return await _entities
                .Where(predicate)
                .Include(g => g.Teacher)
                .Include(g => g.Student)
                .ToListAsync();
        }

        public IQueryable<Grade> QueryWithTeacherAndStudent()
        {
            return _entities.Include(g => g.Teacher).Include(g => g.Student);
        }
    }
}
