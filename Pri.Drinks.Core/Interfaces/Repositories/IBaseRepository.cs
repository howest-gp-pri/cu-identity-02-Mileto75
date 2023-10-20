using Pri.Drinks.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> 
        where T : BaseEntity
    {
        //CRUD
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> CreateAsync(T toCreate);
        Task<bool> UpdateAsync(T toUpdate);
        Task<bool> DeleteAsync(T toDelete);
        IQueryable<T> GetAll();
        Task<bool> SaveChangesAsync();
    }
}
