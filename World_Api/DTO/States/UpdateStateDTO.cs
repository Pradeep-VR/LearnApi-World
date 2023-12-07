namespace World_Api.DTO.States
{
    public class UpdateStateDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Population { get; set; }

        public int CountryId { get; set; }
    }
}
