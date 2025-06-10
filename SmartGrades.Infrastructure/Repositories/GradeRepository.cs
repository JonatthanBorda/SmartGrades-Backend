using Microsoft.EntityFrameworkCore;
using SmartGrades.Domain.Entities;
using SmartGrades.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
