using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using World_Api.Data;
using World_Api.DTO.Country;
using World_Api.Models;

namespace World_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CountryController(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;            
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<GetCountryDTO>> GetAll()
        {
            var cntry = _dbContext.Countries.ToList();

            var cntryDTO = _mapper.Map<List<GetCountryDTO>>(cntry);

            if(cntry == null)
            {
                return NoContent();
            }
            return Ok(cntryDTO);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<GetCountryDTO> GetById(int id)
        {
            var cntry = _dbContext.Countries.Find(id);
            var cntryDTO = _mapper.Map<GetCountryDTO>(cntry);
            if(cntry == null)
            {
                return NoContent();
            }
            return Ok(cntryDTO);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<Country> Create([FromBody] CreateCountryDTO countryDTO)
        {
            var res = _dbContext.Countries.AsQueryable().Where(x => x.Name.ToLower().Trim() == countryDTO.Name).Any();

            if(res == true)
            {
                return Conflict("Country Alreay Exist");
            }
            ///------This is HardCore
            ///So we use Autto mapper..
            
            //Country country = new Country();
            //country.Name = countryDTO.Name;
            //country.ShortName = countryDTO.ShortName;
            //country.CountryCode = countryDTO.CountryCode;

            ///------This is called automapper
            var country = _mapper.Map<Country>(countryDTO);

            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();
            return CreatedAtAction("GetById", new {id=country.Id},country);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<Country> Update(int id,[FromBody] UpdateCountryDTO countryDto)
        {
            if(countryDto == null || id != countryDto.Id)
            {
                return BadRequest();
            }

            //var cntryFrmDb = _dbContext.Countries.Find(id);

            //if(cntryFrmDb == null)
            //{
            //    return NotFound();
            //}

            //cntryFrmDb.Name = country.Name;
            //cntryFrmDb.ShortName = country.ShortName;
            //cntryFrmDb.CountryCode = country.CountryCode;
            
            var cntry = _mapper.Map<Country>(countryDto);

            _dbContext.Countries.Update(cntry);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var cntry = _dbContext.Countries.Find(id);
            if(cntry == null)
            {
                return NotFound();
            }

            _dbContext.Countries.Remove(cntry);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
