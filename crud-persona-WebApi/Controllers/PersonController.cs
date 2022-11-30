using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CrudPersonasWebApi.Dtos;
using CrudPersonasWebApi.Models;
using CrudPersonasWebApi.ResponseModel;
using CrudPersonasWebApi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudPersonasWebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IMapper mapper;
        private readonly IContactMeansPersonService contactMeansPersonService;

        public PersonsController(IPersonService personService, IMapper mapper, IContactMeansPersonService contactMeansPersonService)
        {
            this.personService = personService;
            this.mapper = mapper;
            this.contactMeansPersonService = contactMeansPersonService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<PersonDto>>>> Get()
        {
            var response = new ResponseModel<IEnumerable<PersonDto>>();
            try
            {
                var persons = await this.personService.GetPersons();
                response.Data = this.mapper.Map<IEnumerable<PersonDto>>(persons);
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
                response.Succeeded = false;
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{PersonId}")]
        public async Task<ActionResult<ResponseModel<PersonDto>>> GetById(int PersonId)
        {
            var response = new ResponseModel<PersonDto>();
            try
            {
                var person = await this.personService.GetPersonsById(PersonId);
                if(person is null)
                {
                    response.Succeeded = true;
                    response.Message = "Persona no encontrada";
                    return NotFound(response);
                }
                response.Data = this.mapper.Map<PersonDto>(person);
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
                response.Succeeded = false;
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] PersonDto person)
        {
            var response = new ResponseModel<int>();
            try
            {
                var identity = await this.personService.AddPerson(this.mapper.Map<Person>(person));
                response.Data = identity;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
                response.Succeeded = false;
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody] PersonDto persondto)
        {
            var response = new ResponseModel<int>();
            try
            {
                var person = this.mapper.Map<Person>(persondto);

                var identity = await this.personService.UpdatePerson(person);
                await this.contactMeansPersonService.UpdateContacMeanPerson(person.ContactMeansPeople.ToList());
                response.Data = identity;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
                response.Succeeded = false;
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<int>> Delete(int PersonId)
        {
            var response = new ResponseModel<int>();
            try
            {
                var person = await this.personService.GetPersonsById(PersonId);
                if (person is null)
                {
                    response.Succeeded = true;
                    response.Message = "Persona no encontrada";
                    return NotFound(response);
                }
                await this.contactMeansPersonService.DeleteAllByPersonId(person.PersonId);
                var identity = await this.personService.DeletePerson(person);
                response.Data = identity;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
                response.Succeeded = false;
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
