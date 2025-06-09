using SmartGrades.Domain.Entities;
using SmartGrades.Application.DTOs;

namespace SmartGrades.Application.Interfaces
{
    public interface INotaService
    {
        Task<IEnumerable<Nota>> GetAllAsync();
        Task<Nota> GetByIdAsync(int id);
        Task AddAsync(Nota nota);
        Task UpdateAsync(Nota nota);
        Task DeleteAsync(int id);
        Task<IEnumerable<Nota>> GetNotasByEstudianteAsync(int idEstudiante);
        Task<IEnumerable<Nota>> GetNotasByProfesorAsync(int idProfesor);
        Task<PagedResult<Nota>> GetFilteredAsync(
            string? nombre = null,
            int? idEstudiante = null,
            int? idProfesor = null,
            string? orderBy = "Id",
            bool desc = false,
            int page = 1,
            int pageSize = 10
        );
    }
}
