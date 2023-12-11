using World_Api.Models;

namespace World_Api.Repository.IRepository
{
    public interface ILoginRegRepository : IGenericRepository<LoginRegistration>
    {
        Task<LoginRegistration> GetByUserName(string name);

        Task<List<LoginRegistration>> GetActiveUser(bool sts);

        Task update(LoginRegistration entity);

    }
}
