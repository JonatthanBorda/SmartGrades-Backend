using SmartGrades.Application.Interfaces;
using SmartGrades.Domain.Entities;
using SmartGrades.Domain.Interfaces;
using System.Linq.Dynamic.Core;


namespace SmartGrades.Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EstudianteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _unitOfWork.Estudiantes.GetAllAsync();
        }

        public async Task<Estudiante> GetByIdAsync(int id)
        {
            return await _unitOfWork.Estudiantes.GetByIdAsync(id);
        }

        public async Task AddAsync(Estudiante estudiante)
        {
            await _unitOfWork.Estudiantes.AddAsync(estudiante);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            _unitOfWork.Estudiantes.Update(estudiante);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            //1. Validación de estudiante existente:
            var estudiante = await _unitOfWork.Estudiantes.GetByIdAsync(id);
            if (estudiante != null)
            {
                //2. Validación de notas asociadas:
                var notasAsociadas = (await _unitOfWork.Notas.FindAsync(n => n.IdEstudiante == id)).Any();
                if (notasAsociadas)
                {
                    throw new InvalidOperationException("El estudiante no se puede borrar porque tiene notas asociadas.");
                }

                _unitOfWork.Estudiantes.Remove(estudiante);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<DTOs.PagedResult<Estudiante>> GetFilteredAsync(
            string? nombre,
            string? orderBy = "Id",
            bool desc = false,
            int page = 1,
            int pageSize = 10
        )
        {
            var query = _unitOfWork.Estudiantes.Query();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(e => e.Nombre.Contains(nombre));

            query = query.OrderBy($"{orderBy} {(desc ? "descending" : "ascending")}");

            var total = query.Count();

            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new DTOs.PagedResult<Estudiante>
            {
                Items = items,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
