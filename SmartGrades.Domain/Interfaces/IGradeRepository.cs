using SmartGrades.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Domain.Interfaces
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Task<IEnumerable<Grade>> GetAllWithTeacherAndStudentAsync();
    }
}
