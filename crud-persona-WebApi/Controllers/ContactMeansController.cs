using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CrudPersonasWebApi.Dtos;
using CrudPersonasWebApi.Models;
using CrudPersonasWebApi.ResponseModel;
using CrudPersonasWebApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudPersonasWebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactMeansController : ControllerBase
    {
        private readonly IContactMeanService contactMeanService;
        private readonly IMapper mapper;

        public ContactMeansController(IContactMeanService contactMeanService, IMapper mapper)
        {
            this.contactMeanService = contactMeanService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<ContactMeanDto>>>> Get()
        {
            var response = new ResponseModel<IEnumerable<ContactMeanDto>>();
            try
            {
                var contactMeans = await this.contactMeanService.GetContactMean();
                response.Data = this.mapper.Map<IEnumerable<ContactMeanDto>>(contactMeans);
            }
            catch (System.Exception ex)
            {
                response.Message = ex.Message;
                response.Succeeded = false;
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("ContactMeanId")]
        public async Task<ActionResult<ResponseModel<ContactMeanDto>>> GetById(int ContactMeanId)
        {
            var response = new ResponseModel<ContactMeanDto>();
            try
            {
                var contactMean = await this.contactMeanService.GetContactMeanById(ContactMeanId);
                if (contactMean is null)
                {
                    response.Succeeded = true;
                    response.Message = "Medio de contacto no encontrado";
                    return NotFound(response);
                }
                response.Data = this.mapper.Map<ContactMeanDto>(contactMean);
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
        public async Task<ActionResult<int>> Post([FromBody] ContactMeanDto contactMean)
        {
            var response = new ResponseModel<int>();
            try
            {
                var identity = await this.contactMeanService.AddContactMean(this.mapper.Map<ContactMean>(contactMean));
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
        public async Task<ActionResult<int>> Update([FromBody] ContactMeanDto ContactMean)
        {
            var response = new ResponseModel<int>();
            try
            {
                var identity = await this.contactMeanService.updateContactMean(this.mapper.Map<ContactMean>(ContactMean));
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
        public async Task<ActionResult<int>> Delete(int ContactMeanId)
        {
            var response = new ResponseModel<int>();
            try
            {
                var contactMean = await this.contactMeanService.GetContactMeanById(ContactMeanId);
                if (contactMean is null)
                {
                    response.Succeeded = true;
                    response.Message = "Persona no encontrada";
                    return NotFound(response);
                }
                var identity = await this.contactMeanService.DeleteContactMean(contactMean);
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
