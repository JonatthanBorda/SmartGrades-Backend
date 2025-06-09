using SmartGrades.Domain.Entities;
using SmartGrades.Application.DTOs;

namespace SmartGrades.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsync(int id);
        Task AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(int id);

        Task<PagedResult<Teacher>> GetFilteredAsync(
           string? name,
           string? orderBy = "Id",
           bool desc = false,
           int page = 1,
           int pageSize = 10
       );
    }
}
