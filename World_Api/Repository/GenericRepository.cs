using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using World_Api.Data;
using World_Api.Repository.IRepository;

namespace World_Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(T entity)
        {
            await _dbContext.AddAsync(entity);
            await Save();
        }

        public async Task Delete(T entity)
        {
            _dbContext.Remove(entity);
            await Save();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public bool IsRecordExists(Expression<Func<T, bool>> condition)
        {
            var res = _dbContext.Set<T>().AsQueryable().Where(condition).Any();
            return res;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
