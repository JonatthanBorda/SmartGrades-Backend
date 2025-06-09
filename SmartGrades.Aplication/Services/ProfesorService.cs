using SmartGrades.Application.DTOs;
using SmartGrades.Application.Interfaces;
using SmartGrades.Domain.Entities;
using SmartGrades.Domain.Interfaces;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Application.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfesorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Profesor>> GetAllAsync()
        {
            return await _unitOfWork.Profesores.GetAllAsync();
        }

        public async Task<Profesor> GetByIdAsync(int id)
        {
            return await _unitOfWork.Profesores.GetByIdAsync(id);
        }

        public async Task AddAsync(Profesor profesor)
        {
            await _unitOfWork.Profesores.AddAsync(profesor);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Profesor profesor)
        {
            _unitOfWork.Profesores.Update(profesor);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var profesor = await _unitOfWork.Profesores.GetByIdAsync(id);
            if (profesor != null)
            {
                _unitOfWork.Profesores.Remove(profesor);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<DTOs.PagedResult<Profesor>> GetFilteredAsync(string? nombre, string? orderBy = "Id", 
                                                                    bool desc = false,
                                                                    int page = 1, 
                                                                    int pageSize = 10)
        {
            var query = _unitOfWork.Profesores.Query();

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(p => p.Nombre.Contains(nombre));

            query = query.OrderBy($"{orderBy} {(desc ? "descending" : "ascending")}");

            var total = query.Count();

            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new DTOs.PagedResult<Profesor>
            {
                Items = items,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
