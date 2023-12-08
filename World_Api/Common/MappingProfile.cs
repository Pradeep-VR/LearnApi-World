using AutoMapper;
using World_Api.DTO.Country;
using World_Api.DTO.LoginRegistration;
using World_Api.DTO.States;
using World_Api.Models;

namespace World_Api.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ///-Source , Distination
            ///--Country
            CreateMap<Country, CreateCountryDTO>().ReverseMap();

            CreateMap<Country, GetCountryDTO>().ReverseMap();

            CreateMap<Country,UpdateCountryDTO>().ReverseMap();

            ///---State
            CreateMap<States, CreateStateDTO>().ReverseMap();

            CreateMap<States, GetStateDTO>().ReverseMap();

            CreateMap<States,UpdateStateDTO>().ReverseMap();

            ///----LoginRegistration
            CreateMap<LoginRegistration, GetLoginRegDTO>().ReverseMap();

            CreateMap<LoginRegistration, PostLoginRegDTO>().ReverseMap();
        }
    }
}
