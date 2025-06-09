using SmartGrades.Application.Interfaces;
using SmartGrades.Domain.Entities;
using SmartGrades.Domain.Interfaces;
using System.Linq.Dynamic.Core;


namespace SmartGrades.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _unitOfWork.Students.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _unitOfWork.Students.GetByIdAsync(id);
        }

        public async Task AddAsync(Student student)
        {
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _unitOfWork.Students.Update(student);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            //1. Validación de student existente:
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student != null)
            {
                //2. Validación de notas asociadas:
                var notasAsociadas = (await _unitOfWork.Grades.FindAsync(n => n.IdStudent == id)).Any();
                if (notasAsociadas)
                {
                    throw new InvalidOperationException("El student no se puede borrar porque tiene notas asociadas.");
                }

                _unitOfWork.Students.Remove(student);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<DTOs.PagedResult<Student>> GetFilteredAsync(
            string? name,
            string? orderBy = "Id",
            bool desc = false,
            int page = 1,
            int pageSize = 10
        )
        {
            var query = _unitOfWork.Students.Query();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            query = query.OrderBy($"{orderBy} {(desc ? "descending" : "ascending")}");

            var total = query.Count();

            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new DTOs.PagedResult<Student>
            {
                Items = items,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
