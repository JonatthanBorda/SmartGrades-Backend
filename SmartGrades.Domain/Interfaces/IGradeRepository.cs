using SmartGrades.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Domain.Interfaces
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Task<IEnumerable<Grade>> GetAllWithTeacherAndStudentAsync();
        Task<Grade?> GetByIdWithTeacherAndStudentAsync(int id);
        Task<IEnumerable<Grade>> FindAsyncWithTeacherAndStudentAsync(Expression<Func<Grade, bool>> predicate);
        IQueryable<Grade> QueryWithTeacherAndStudent();
    }
}
