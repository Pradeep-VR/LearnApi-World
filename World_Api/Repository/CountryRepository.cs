using Microsoft.EntityFrameworkCore;
using World_Api.Data;
using World_Api.Models;
using World_Api.Repository.IRepository;

namespace World_Api.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _DbContext;

        public CountryRepository(ApplicationDbContext DbContext):base(DbContext)
        {
            _DbContext = DbContext;
        }

        //public async Task<List<Country>> GETALL()
        //{
        //    List<Country> contries = await _DbContext.Countries.ToListAsync();
        //    return contries;
        //}

        //public async Task<Country> GETBYID(int id)
        //{
        //    Country country = await _DbContext.Countries.FindAsync(id);
        //    return country;
        //}

        //public async Task CREATE(Country entity)
        //{
        //    await _DbContext.Countries.AddAsync(entity);
        //    await SAVE();
        //}

        public async Task UPDATE(Country entity)
        {
            _DbContext.Countries.Update(entity);
            await _DbContext.SaveChangesAsync();
        }

        //public async Task DELETE(Country entity)
        //{
        //    _DbContext.Countries.Remove(entity);
        //    await SAVE();
        //}

        //public async Task SAVE()
        //{
        //    await _DbContext.SaveChangesAsync();
        //}

        //public bool IsCountryExists(string name)
        //{
        //    var res = _DbContext.Countries.AsQueryable().Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();
        //    return res;
        //}
    }
}
