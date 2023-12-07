using World_Api.Models;

namespace World_Api.Repository.IRepository
{
    public interface IStateRepository : IGenericRepository<States>
    {
        //Task<List<States>> GetAll();
        
        //Task<States> GetById(int id);       

        //Task Create(States state);

        Task Update(States state);

        //Task Delete(States state);

        //Task Save();

        //bool IsStateExists(string name);

    }
}
