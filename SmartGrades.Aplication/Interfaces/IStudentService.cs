using SmartGrades.Domain.Entities;
using SmartGrades.Application.DTOs;

namespace SmartGrades.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);

        Task<PagedResult<Student>> GetFilteredAsync(
            string? name,
            string? orderBy = "Id",
            bool desc = false,
            int page = 1,
            int pageSize = 10
        );
    }
}
