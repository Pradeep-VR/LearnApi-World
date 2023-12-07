using System.Linq.Expressions;

namespace World_Api.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        Task<T> GetById(int id);

        Task Create(T state);

        //Task Update(T state);

        Task Delete(T state);

        Task Save();

        //bool IsStateExists(string name);
        bool IsRecordExists(Expression<Func<T, bool>> condition);
    }
}
