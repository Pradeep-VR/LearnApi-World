using Microsoft.EntityFrameworkCore;
using World_Api.Data;
using World_Api.Models;
using World_Api.Repository.IRepository;

namespace World_Api.Repository
{
    public class LoginRegRepository : GenericRepository<LoginRegistration>, ILoginRegRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LoginRegRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LoginRegistration>> GetActiveUser(bool sts)
        {
            List<LoginRegistration> logReg = await _dbContext.loginRegistrations.Where(lr => lr.ActiveSts == sts).ToListAsync();
            return logReg;
        }

        public async Task<LoginRegistration> GetByUserName(string name)
        {
            LoginRegistration logReg = await _dbContext.loginRegistrations.FirstOrDefaultAsync(lr => lr.userName ==  name);
            return logReg;
        }

        public async Task update(LoginRegistration entity)
        {
            _dbContext.loginRegistrations.Update(entity);
            await _dbContext.SaveChangesAsync();

        }
    }
}
