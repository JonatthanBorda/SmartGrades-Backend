using Microsoft.EntityFrameworkCore;
using SmartGrades.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartGrades.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await _entities.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _entities.ToListAsync();

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _entities.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) => await _entities.AddAsync(entity);

        public void Update(T entity) => _entities.Update(entity);

        public void Remove(T entity) => _entities.Remove(entity);

        //Para filtrado y paginación:
        public IQueryable<T> Query()
        {
            return _entities.AsQueryable();
        }
    }
}
