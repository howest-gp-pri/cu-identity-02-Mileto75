using Microsoft.AspNetCore.Http;
using Pri.Drinks.Core.Entities;
using Pri.Drinks.Core.Interfaces.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Core.Interfaces.Services
{
    public interface IDrinkService
    {
        Task<ResultModel<Drink>> GetAllAsync();
        Task<ResultModel<Drink>> GetByIdAsync(int id);
        Task<ResultModel<Drink>> CreateAsync(string name, int categoryId,
            int alcoholPercentage, IEnumerable<int> propertyIds,IFormFile formFile);
        Task<ResultModel<Drink>> UpdateAsync(int id, string name, int categoryId,
            int alcoholPercentage, IEnumerable<int> propertyIds);
        Task<ResultModel<Drink>> SearchByNameAsync(string search);
        Task<bool> ExistsAsync(int id);
        Task<ResultModel<Drink>> DeleteAsync(int id);
        //a quick fix to get a list of categories and properties
        Task<ResultModel<Property>> GetPropertiesAsync();
        Task<ResultModel<Category>> GetCategoriesAsync();
    }
}
