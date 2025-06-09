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
        IRepository<Estudiante> Estudiantes { get; }
        IRepository<Profesor> Profesores { get; }
        IRepository<Nota> Notas { get; }
        Task<int> CompleteAsync();
    }
}
