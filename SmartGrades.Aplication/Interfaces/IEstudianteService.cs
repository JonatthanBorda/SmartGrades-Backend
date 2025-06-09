using SmartGrades.Domain.Entities;
using SmartGrades.Application.DTOs;

namespace SmartGrades.Application.Interfaces
{
    public interface IEstudianteService
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante> GetByIdAsync(int id);
        Task AddAsync(Estudiante estudiante);
        Task UpdateAsync(Estudiante estudiante);
        Task DeleteAsync(int id);

        Task<PagedResult<Estudiante>> GetFilteredAsync(
            string? nombre,
            string? orderBy = "Id",
            bool desc = false,
            int page = 1,
            int pageSize = 10
        );
    }
}
