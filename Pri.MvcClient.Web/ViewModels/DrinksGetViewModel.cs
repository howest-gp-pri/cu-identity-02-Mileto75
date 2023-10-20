namespace Pri.MvcClient.Web.ViewModels
{
    public class DrinksGetViewModel : BaseDrinkViewModel
    {
        public BaseViewModel Category { get; set; }
        public IEnumerable<BaseViewModel> Properties { get; set; }
    }
}
