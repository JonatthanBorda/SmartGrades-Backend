using SmartGrades.Domain.Entities;
using SmartGrades.Application.DTOs;

namespace SmartGrades.Application.Interfaces
{
    public interface IProfesorService
    {
        Task<IEnumerable<Profesor>> GetAllAsync();
        Task<Profesor> GetByIdAsync(int id);
        Task AddAsync(Profesor profesor);
        Task UpdateAsync(Profesor profesor);
        Task DeleteAsync(int id);

        Task<PagedResult<Profesor>> GetFilteredAsync(
           string? nombre,
           string? orderBy = "Id",
           bool desc = false,
           int page = 1,
           int pageSize = 10
       );
    }
}
