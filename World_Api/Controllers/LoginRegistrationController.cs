using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using World_Api.DTO.Country;
using World_Api.DTO.LoginRegistration;
using World_Api.Models;
using World_Api.Repository;
using World_Api.Repository.IRepository;

namespace World_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginRegistrationController : ControllerBase
    {
        private readonly ILoginRegRepository _iloginregRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginRegistrationController> _logger;

        public LoginRegistrationController(ILoginRegRepository iloginregRepository, IMapper mapper,ILogger<LoginRegistrationController> logger)
        {
            _iloginregRepository = iloginregRepository;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<GetLoginRegDTO>>> GetAll()
        {
            var logreg = await _iloginregRepository.GetAll();

            if(logreg == null)
            {
                _logger.LogError("Error While Get A Record");
                return NoContent();
            }

            var logregDTO = _mapper.Map<List<GetLoginRegDTO>>(logreg);
            return Ok(logregDTO);
        }


        /*[HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<GetLoginRegDTO>> GetByuserName(string UserName)
        {
            var cntry = await _iloginregRepository.GetById(UserName);

            if (cntry == null)
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

        public async Task<ActionResult<LoginRegistration>> Create([FromBody] PostLoginRegDTO postLoginDTO)
        {
            var result = _iloginregRepository.IsRecordExists(x => x.userName == postLoginDTO.userName);

            if(result == true)
            {
                return Conflict("User Already Exist");
            }

            var user = _mapper.Map<LoginRegistration>(postLoginDTO);

            await _iloginregRepository.Create(user);
            await _iloginregRepository.Save();
            return CreatedAtAction("GetByuserName",new {userName = user.userName},user);
        }*/
    }
}
