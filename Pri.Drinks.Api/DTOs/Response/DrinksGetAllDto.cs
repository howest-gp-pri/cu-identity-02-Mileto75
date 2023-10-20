namespace Pri.Drinks.Api.DTOs.Response
{
    public class DrinksGetAllDto
    {
        public IEnumerable<DrinksGetByIdDto> Drinks { get; set; }
    }
}
