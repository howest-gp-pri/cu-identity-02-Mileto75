using Pri.Drinks.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Core.Interfaces.Repositories
{
    public interface IDrinkRepository : IBaseRepository<Drink>
    {
        Task<IEnumerable<Drink>> GetByCategory(int id);
    }
}
