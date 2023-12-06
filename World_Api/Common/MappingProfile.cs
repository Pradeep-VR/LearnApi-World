using AutoMapper;
using World_Api.DTO.Country;
using World_Api.Models;

namespace World_Api.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ///-Source , Distination
            CreateMap<Country, CreateCountryDTO>().ReverseMap();

            CreateMap<Country, GetCountryDTO>().ReverseMap();

            CreateMap<Country,UpdateCountryDTO>().ReverseMap();

        }
    }
}
