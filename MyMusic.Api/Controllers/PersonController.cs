using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Api.Resources;
using MyMusic.Api.Validations;
using MyMusic.Core.Models;
using MyMusic.Core.Services;
using MyMusic.Services;

namespace MyMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonController(IPersonService personService, IMapper mapper)
        {
            this._personService = personService;
            this._mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPeople()
        {
            var people = await _personService.GetAll();
            var personResources = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonResource>>(people);
            return Ok(personResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonResource>> GetById(int id)
        {
            var person = await _personService.GetById(id);
            var personResource = _mapper.Map<Person, PersonResource>(person);

            return Ok(personResource);
        }


        [HttpPost("")]
        public async Task<ActionResult<PersonResource>> CreatePerson([FromBody] SavePersonResource savePersonResource)
        {
            var validator = new SavePersonResourceValidator();
            var validationResult = await validator.ValidateAsync(savePersonResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var personToCreate = _mapper.Map<SavePersonResource, Person>(savePersonResource);

            var newPerson = await _personService.CreatePerson(personToCreate);

            var person = await _personService.GetById(newPerson.Id);

            var personResource = _mapper.Map<Person, PersonResource>(person);

            return Ok(personResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PersonResource>> UpdatePerson(int id, [FromBody] SavePersonResource savePersonResource)
        {
            var validator = new SavePersonResourceValidator();
            var validationResult = await validator.ValidateAsync(savePersonResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var personToBeUpdate = await _personService.GetById(id);

            if (personToBeUpdate == null)
                return NotFound();

            var person = _mapper.Map<SavePersonResource, Person>(savePersonResource);

            await _personService.Update(personToBeUpdate, person);

            var updatedPerson = await _personService.GetById(id);
            var updatedPersonResource = _mapper.Map<Person, PersonResource>(updatedPerson);

            return Ok(updatedPersonResource);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            if (id == 0)
                return BadRequest();

            var person = await _personService.GetById(id);

            if (person == null)
                return NotFound();

            await _personService.Delete(person);

            return NoContent();
        }
    }
}