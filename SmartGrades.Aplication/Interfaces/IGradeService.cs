using SmartGrades.Domain.Entities;
using SmartGrades.Application.DTOs;
using SmartGrades.Application.DTOs.Grade;

namespace SmartGrades.Application.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<Grade>> GetAllAsync();
        Task<Grade?> GetByIdAsync(int id);
        Task AddAsync(Grade grade);
        Task UpdateAsync(Grade grade);
        Task DeleteAsync(int id);
        Task<IEnumerable<Grade>> GetNotasByEstudianteAsync(int idStudent);
        Task<IEnumerable<Grade>> GetNotasByProfesorAsync(int idTeacher);
        Task<PagedResult<GradeDTOs>> GetFilteredAsync(
            string? name = null,
            int? idStudent = null,
            int? idTeacher = null,
            string? orderBy = "Id",
            bool desc = false,
            int page = 1,
            int pageSize = 10
        );
    }
}
