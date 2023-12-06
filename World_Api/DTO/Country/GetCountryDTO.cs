using System.ComponentModel.DataAnnotations;

namespace World_Api.DTO.Country
{
    public class GetCountryDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string CountryCode { get; set; }
    }
}
