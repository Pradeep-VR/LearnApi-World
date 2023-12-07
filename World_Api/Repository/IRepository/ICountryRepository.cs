using World_Api.Models;

namespace World_Api.Repository.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        //Task<List<Country>> GETALL();
        //Task<Country> GETBYID(int id);
        //Task CREATE (Country entity);
        Task UPDATE(Country entity);
        //Task DELETE (Country entity);
        //Task SAVE();

        //bool IsCountryExists(string name);
    }
}
