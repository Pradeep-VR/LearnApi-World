using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using World_Api.DTO.Country;
using World_Api.DTO.States;
using World_Api.Migrations;
using World_Api.Models;
using World_Api.Repository.IRepository;

namespace World_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository _stateRepos;
        private readonly IMapper _mapper;

        public StateController(IStateRepository stateRepos,IMapper mapper)
        {
            _stateRepos = stateRepos;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<GetStateDTO>>> GetAll()
        {
            var state = await _stateRepos.GetAll();
            var stateDTO = _mapper.Map<List<GetStateDTO>>(state);

            if(state == null)
            {
                return NoContent();
            }
            return Ok(stateDTO);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<GetStateDTO>> GetById(int id)
        {
            var state = await _stateRepos.GetById(id);
            var stateDTO=_mapper.Map<GetStateDTO>(state);
            if(state == null)
            {
                return NoContent();
            }
            return Ok(stateDTO);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<ActionResult<States>> Create([FromBody] CreateStateDTO stateDTO)
        {
            var res = _stateRepos.IsRecordExists(x=>x.Name == stateDTO.Name);
            
            if(res == true)
            {
                return Conflict("State Already Exist");
            }

            var state = _mapper.Map<States>(stateDTO);

            await _stateRepos.Create(state);
            await _stateRepos.Save();
            return CreatedAtAction("GetById", new { id = state }, state);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<States>> Update(int id, [FromBody] UpdateStateDTO stateDTO)
        {
            if(stateDTO == null || id != stateDTO.Id)
            {
                return BadRequest();
            }

            var state = _mapper.Map<States>(stateDTO);

            await _stateRepos.Update(state);
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

            var state = await _stateRepos.GetById(id);
            if(state == null)
            {
                return NotFound();
            }

            await _stateRepos.Delete(state);
            return NoContent();
        }
    }
}
