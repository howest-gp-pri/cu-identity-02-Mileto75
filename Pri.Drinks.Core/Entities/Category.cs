namespace Pri.Drinks.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Drink> Drinks { get; set; }
    }
}