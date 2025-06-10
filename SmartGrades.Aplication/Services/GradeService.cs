using SmartGrades.Application.DTOs.Estudiante;
using SmartGrades.Application.DTOs.Grade;
using SmartGrades.Application.DTOs.Nota;
using SmartGrades.Application.DTOs.Profesor;
using SmartGrades.Application.Interfaces;
using SmartGrades.Domain.Entities;
using SmartGrades.Domain.Interfaces;
using System.Linq.Dynamic.Core;

namespace SmartGrades.Application.Services
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Grade>> GetAllAsync()
        {
            return await _unitOfWork.Grades.GetAllWithTeacherAndStudentAsync();
        }

        public async Task<Grade?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Grades.GetByIdWithTeacherAndStudentAsync(id);
        }

        public async Task AddAsync(Grade grade)
        {
            await _unitOfWork.Grades.AddAsync(grade);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Grade grade)
        {
            _unitOfWork.Grades.Update(grade);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var grade = await _unitOfWork.Grades.GetByIdAsync(id);
            if (grade != null)
            {
                _unitOfWork.Grades.Remove(grade);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<IEnumerable<Grade>> GetNotasByEstudianteAsync(int idEstudiante)
        {
            return await _unitOfWork.Grades.FindAsyncWithTeacherAndStudentAsync(n => n.IdStudent == idEstudiante);
        }

        public async Task<IEnumerable<Grade>> GetNotasByProfesorAsync(int idProfesor)
        {
            return await _unitOfWork.Grades.FindAsyncWithTeacherAndStudentAsync(n => n.IdTeacher == idProfesor);
        }

        public async Task<DTOs.PagedResult<GradeDTOs>> GetFilteredAsync(
            string? name = null,
            int? idStudent = null,
            int? idTeacher = null,
            string? orderBy = "Id",
            bool desc = false,
            int page = 1,
            int pageSize = 10
        )
        {
            var query = _unitOfWork.Grades.QueryWithTeacherAndStudent();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(n =>
                    n.Name.Contains(name) || n.Teacher.Name.Contains(name) ||
                    n.Student.Name.Contains(name) || n.Value.ToString().Contains(name));

            if (idStudent.HasValue)
                query = query.Where(n => n.IdStudent == idStudent.Value);

            if (idTeacher.HasValue)
                query = query.Where(n => n.IdTeacher == idTeacher.Value);

            query = query.OrderBy($"{orderBy} {(desc ? "descending" : "ascending")}");

            var total = query.Count();

            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var itemList = items.Select(item => new GradeDTOs(
                                item.Id,
                                item.Name,
                                item.Value,
                                new StudentDTOs(
                                    item.Student.Id,
                                    item.Student.Name
                                ),
                                new TeacherDTOs(
                                    item.Teacher.Id,
                                    item.Teacher.Name
                                )
            ));

            return new DTOs.PagedResult<GradeDTOs>
            {
                Items = itemList,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
