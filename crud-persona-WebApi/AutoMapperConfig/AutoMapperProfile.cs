using AutoMapper;
using CrudPersonasWebApi.Dtos;
using CrudPersonasWebApi.Models;

namespace CrudPersonasWebApi.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<ContactMeansPerson, ContactMeansPersonDto>().ReverseMap();
            CreateMap<ContactMean, ContactMeanDto>().ReverseMap();  
        }
    }
}
