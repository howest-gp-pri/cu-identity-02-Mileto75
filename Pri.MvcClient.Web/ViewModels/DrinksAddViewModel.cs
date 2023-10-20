using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Pri.MvcClient.Web.ViewModels
{
    public class DrinksAddViewModel
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Must have category!")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        [Range(0, 60)]
        public int AlcoholPercentage { get; set; }
        [Required(ErrorMessage = "Please provide at least one property")]
        [Display(Name = "Properties")]
        public IEnumerable<int> PropertyIds { get; set; }
        public IEnumerable<SelectListItem> Properties { get; set; }
        [Required]
        public  IFormFile Image { get; set; }
    }
}
