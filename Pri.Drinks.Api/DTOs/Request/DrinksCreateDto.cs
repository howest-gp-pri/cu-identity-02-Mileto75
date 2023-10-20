using System.ComponentModel.DataAnnotations;

namespace Pri.Drinks.Api.DTOs.Request
{
    public class DrinksCreateDto
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Must have category!")]
        public int CategoryId { get; set; }
        [Range(0,60)]
        public int AlcoholPercentage { get; set; }
        [Required]
        public IEnumerable<int> PropertyIds { get; set; }
        public IFormFile Image { get; set; }
    }
}
