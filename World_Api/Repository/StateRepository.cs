using Microsoft.EntityFrameworkCore;
using World_Api.Data;
using World_Api.Migrations;
using World_Api.Models;
using World_Api.Repository.IRepository;

namespace World_Api.Repository
{
    public class StateRepository : GenericRepository<States> , IStateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task Create(States entity)
        //{
        //    await _dbContext.States.AddAsync(entity);
        //    await Save();
        //}

        //public async Task Delete(States entity)
        //{
        //    _dbContext.States.Remove(entity);
        //    await Save();
        //}

        //public async Task<List<States>> GetAll()
        //{
        //    List<States> states = await _dbContext.States.ToListAsync();
        //    return states;
        //}

        //public async Task<States> GetById(int id)
        //{
        //    States state = await _dbContext.States.FindAsync(id);
        //    return state;
        //}

        
        //public bool IsStateExists(string name)
        //{
        //    var res = _dbContext.States.AsQueryable().Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();
        //    return res;
        //}

        //public async Task Save()
        //{
        //    await _dbContext.SaveChangesAsync();
        //}

        public async Task Update(States state)
        {
            _dbContext.Update(state);
            await _dbContext.SaveChangesAsync();                 
        }
    }
}
