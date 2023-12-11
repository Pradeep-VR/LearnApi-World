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


        [HttpGet("sts:bool")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<GetLoginRegDTO>>> GetActiveUser(bool sts)
        {
            var logres = await _iloginregRepository.GetActiveUser(sts);
            if(logres == null)
            {
                _logger.LogError("Error While Get Active Users");
                return NoContent();
            }
            if(logres.Count == 0)
            {
                _logger.LogError("There is Zero Active Users");
                return NoContent();
            }

            var LogResDto = _mapper.Map<List<GetLoginRegDTO>>(logres);
            return Ok(LogResDto);
        }



        [HttpGet("{UserName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<GetLoginRegDTO>> GetByuserName(string UserName)
        {
            var LogRes = await _iloginregRepository.GetByUserName(UserName);

            if (LogRes == null)
            {
                _logger.LogError($"Error While try to Get a User : {UserName}");
                return NoContent();
            }
            var LogResDTO = _mapper.Map<GetLoginRegDTO>(LogRes);
            return Ok(LogResDTO);
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
            return CreatedAtAction("GetByuserName",new {UserName = user.userName},user);
        }

        [HttpDelete("{UserName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteById(string UserName)
        {
            if (UserName == ""||UserName == null)
            {
                return BadRequest();
            }

            var res = await _iloginregRepository.GetByUserName(UserName);
            if(res == null)
            {
                return NotFound();
            }

            await _iloginregRepository.Delete(res);
            return NoContent();
        }

        [HttpPut("{UserName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<LoginRegistration>> Update(string UserName, [FromBody] UpdateLogRegDTO logregDto)
        {
            if (logregDto == null)
            {
                return BadRequest();
            }

            bool res = _iloginregRepository.IsRecordExists(x => x.userName == UserName);

            if(res == false)
            {
                return NotFound();
            }

            var logReg = _mapper.Map<LoginRegistration>(logregDto);

            await _iloginregRepository.update(logReg);
            return NoContent();
        }
    }
}
