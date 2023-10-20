using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Core.Entities
{   
    public class Property : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Drink> Drinks { get; set; }
    }
}
