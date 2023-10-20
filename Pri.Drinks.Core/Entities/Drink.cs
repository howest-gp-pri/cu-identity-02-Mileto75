using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Core.Entities
{
    public class Drink : BaseEntity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId  { get; set; }
        public int AlcoholPercentage { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public ICollection<Property> Properties { get; set; }
        public string Image { get; set; }
    }
}
