using SmartGrades.Application.Interfaces;
using SmartGrades.Domain.Entities;
using SmartGrades.Domain.Interfaces;
using System.Linq.Dynamic.Core;

namespace SmartGrades.Application.Services
{
    public class NotaService : INotaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Nota>> GetAllAsync()
        {
            return await _unitOfWork.Notas.GetAllAsync();
        }

        public async Task<Nota> GetByIdAsync(int id)
        {
            return await _unitOfWork.Notas.GetByIdAsync(id);
        }

        public async Task AddAsync(Nota nota)
        {
            await _unitOfWork.Notas.AddAsync(nota);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Nota nota)
        {
            _unitOfWork.Notas.Update(nota);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var nota = await _unitOfWork.Notas.GetByIdAsync(id);
            if (nota != null)
            {
                _unitOfWork.Notas.Remove(nota);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<IEnumerable<Nota>> GetNotasByEstudianteAsync(int idEstudiante)
        {
            return await _unitOfWork.Notas.FindAsync(n => n.IdEstudiante == idEstudiante);
        }

        public async Task<IEnumerable<Nota>> GetNotasByProfesorAsync(int idProfesor)
        {
            return await _unitOfWork.Notas.FindAsync(n => n.IdProfesor == idProfesor);
        }

        public async Task<DTOs.PagedResult<Nota>> GetFilteredAsync(
            string? nombre = null,
            int? idEstudiante = null,
            int? idProfesor = null,
            string? orderBy = "Id",
            bool desc = false,
            int page = 1,
            int pageSize = 10
        )
        {
            var query = _unitOfWork.Notas.Query();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(n => n.Nombre.Contains(nombre));

            if (idEstudiante.HasValue)
                query = query.Where(n => n.IdEstudiante == idEstudiante.Value);

            if (idProfesor.HasValue)
                query = query.Where(n => n.IdProfesor == idProfesor.Value);

            query = query.OrderBy($"{orderBy} {(desc ? "descending" : "ascending")}");

            var total = query.Count();

            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new DTOs.PagedResult<Nota>
            {
                Items = items,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
