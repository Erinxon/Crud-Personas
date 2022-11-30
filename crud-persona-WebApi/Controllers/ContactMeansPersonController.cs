using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CrudPersonasWebApi.Dtos;
using CrudPersonasWebApi.Models;
using CrudPersonasWebApi.ResponseModel;
using CrudPersonasWebApi.Services;
using System.Threading.Tasks;

namespace CrudPersonasWebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactMeansPersonController : ControllerBase
    {
        private readonly IContactMeansPersonService contactMeansPersonService;
        private readonly IMapper mapper;

        public ContactMeansPersonController(IContactMeansPersonService contactMeansPersonService, IMapper mapper)
        {
            this.contactMeansPersonService = contactMeansPersonService;
            this.mapper = mapper;
        }

        [HttpDelete]
        public async Task<ActionResult<int>> Delete(int contactMeansPeopleId)
        {
            var response = new ResponseModel<int>();
            try
            {
                var contactMeansPeople = await this.contactMeansPersonService.GetContactMeansPeopleById(contactMeansPeopleId);
                if (contactMeansPeople is null)
                {
                    response.Succeeded = true;
                    response.Message = "Medio de contacto no encontrado";
                    return NotFound(response);
                }
                response.Data = await this.contactMeansPersonService.Delete(contactMeansPeople.Id);
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
