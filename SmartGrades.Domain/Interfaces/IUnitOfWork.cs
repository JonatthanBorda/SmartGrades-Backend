using SmartGrades.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }
        IRepository<Teacher> Teachers { get; }
        IGradeRepository Grades { get; }
        Task<int> CompleteAsync();
    }
}
