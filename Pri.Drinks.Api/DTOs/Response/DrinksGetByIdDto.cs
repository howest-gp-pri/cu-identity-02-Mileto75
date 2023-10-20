namespace Pri.Drinks.Api.DTOs.Response
{
    public class DrinksGetByIdDto : BaseDto
    {
        public BaseDto Category { get; set; }
        public IEnumerable<BaseDto> Properties { get; set; }
        public int AlcoholPercentage { get; set; }
        public string ImageUrl { get; set; }
    }
}
