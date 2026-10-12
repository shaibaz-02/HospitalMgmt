using HospitalMgmt.Core.Interfaces;
using HospitalMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HospitalMgmt.Infrastructure.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        protected readonly HospitalDbContext _context;
        protected readonly DbSet<T> _dbset;

        public GenericRepository(HospitalDbContext context)
        {
            _context=context;
            _dbset=context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _dbset.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
    }
}