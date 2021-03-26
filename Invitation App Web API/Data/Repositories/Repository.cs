using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Invitation_App_Web_API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invitation_App_Web_API.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            var dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Added;
            _context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            var dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Added;
            await _context.Set<T>().AddAsync(entity);
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate) => await _context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            return await includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty))
                .FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> AsQueryable() => _context.Set<T>().AsQueryable();

        public void Delete(T entity)
        {
            var dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate);

        public IQueryable<T> GetSet() => _context.Set<T>();

        public void Update(T entity)
        {
            var dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
    }
}
