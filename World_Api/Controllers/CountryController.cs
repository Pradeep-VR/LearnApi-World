using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using World_Api.DTO.Country;
using World_Api.Models;
using World_Api.Repository.IRepository;

namespace World_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        //private readonly ApplicationDbContext _dbContext;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryRepository countryRepository,IMapper mapper,ILogger<CountryController> logger)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetAll()
        {
            var cntry = await _countryRepository.GetAll();

            if(cntry == null)
            {
                _logger.LogError("Error While Get a records");
                return NoContent();
            }
            var cntryDTO = _mapper.Map<List<GetCountryDTO>>(cntry);

            return Ok(cntryDTO);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<GetCountryDTO>> GetById(int id)
        {
            var cntry = await _countryRepository.GetById(id);
            
            if(cntry == null)
            {
                _logger.LogError($"Error While try to Get a record id : {id}");
                return NoContent();
            }
            var cntryDTO = _mapper.Map<GetCountryDTO>(cntry);
            return Ok(cntryDTO);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<ActionResult<Country>> Create([FromBody] CreateCountryDTO countryDTO)
        {
            var res =  _countryRepository.IsRecordExists(x=>x.Name == countryDTO.Name);

            if(res == true)
            {
                return Conflict("Country Already Exist");
            }
            ///------This is HardCore
            ///So we use Autto mapper..
            
            //Country country = new Country();
            //country.Name = countryDTO.Name;
            //country.ShortName = countryDTO.ShortName;
            //country.CountryCode = countryDTO.CountryCode;

            ///------This is called automapper
            var country = _mapper.Map<Country>(countryDTO);

            //_dbContext.Countries.Add(country);
            //_dbContext.SaveChanges();
            await _countryRepository.Create(country);
            await _countryRepository.Save();
            return CreatedAtAction("GetById", new {id=country.Id},country);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<Country>> Update(int id,[FromBody] UpdateCountryDTO countryDto)
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

            await _countryRepository.UPDATE(cntry);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var cntry = await _countryRepository.GetById(id);
            if(cntry == null)
            {
                return NotFound();
            }

            await _countryRepository.Delete(cntry);
            return NoContent();
        }
    }
}
