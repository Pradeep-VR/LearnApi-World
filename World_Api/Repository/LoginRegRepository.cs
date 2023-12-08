using System.Linq.Expressions;
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

        public Task GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        
    }
}
