using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pri.Drinks.Core.Entities;
using Pri.Drinks.Core.Interfaces.Repositories;
using Pri.Drinks.Core.Interfaces.Services;
using Pri.Drinks.Core.Interfaces.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Core.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IFileService _fileService;

        public DrinkService(IDrinkRepository drinkRepository, ICategoryRepository categoryRepository, IPropertyRepository propertyRepository, IFileService fileService)
        {
            _drinkRepository = drinkRepository;
            _categoryRepository = categoryRepository;
            _propertyRepository = propertyRepository;
            _fileService = fileService;
        }

        public async Task<ResultModel<Drink>> CreateAsync(string name, int categoryId, int alcoholPercentage, IEnumerable<int> propertyIds,IFormFile formFile)
        {
            //check if categoryId exists
            var categories = _categoryRepository.GetAll();
            if (!await categories.AnyAsync(c => c.Id == categoryId))
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Unkown category" }
                };
            }
            //check if propertyIds exist
            var properties = _propertyRepository.GetAll();
            if(!propertyIds.All(pi => properties.Any(p => p.Id == pi)))
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Unkown Property" }
                };
            }
            //check if name exists
            if(await _drinkRepository
                .GetAll().AnyAsync(d => d.Name.ToUpper()
                .Equals(name.ToUpper())))
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Name exists!" }
                };
            }
            //call create method
            var drink = new Drink
            {
                Name = name,
                AlcoholPercentage = alcoholPercentage,
                CategoryId = categoryId,
                Properties = _propertyRepository.GetAll()
                    .Where(p => propertyIds.Contains(p.Id)).ToList(),
                Image = await _fileService.StoreFileAsync<Drink>(formFile,"Images")
            };
            if(!await _drinkRepository.CreateAsync(drink))
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Unknown error, please try again later..." }
                };
            }
            return new ResultModel<Drink> 
            { 
                IsSuccess = true
            };
        }

        public async Task<ResultModel<Drink>> DeleteAsync(int id)
        {
            var toDelete = await _drinkRepository.GetByIdAsync(id);
            if(!await _drinkRepository.DeleteAsync(toDelete))
            {
                return new ResultModel<Drink> 
                { 
                    IsSuccess = false,
                    Errors = new List<string>() { "Unkown error..." }
                };
            }
            return new ResultModel<Drink>
            {
                IsSuccess = true
            };
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _drinkRepository
                .GetAll().AnyAsync(d => d.Id == id);
        }

        public async Task<ResultModel<Drink>> GetAllAsync()
        {
            var drinks = await _drinkRepository.GetAllAsync();
            return new ResultModel<Drink>
            {
                IsSuccess = true,
                Items = drinks
            };
        }

        public async Task<ResultModel<Drink>> GetByIdAsync(int id)
        {
            var drink = await _drinkRepository.GetByIdAsync(id);
            if(drink == null)
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Drink not found!" }
                };
            }
            return new ResultModel<Drink>
            {
                IsSuccess = true,
                Items = new List<Drink> { drink },
            };
        }

        public async Task<ResultModel<Category>> GetCategoriesAsync()
        {
            return new ResultModel<Category>
            {
                IsSuccess = true,
                Items = await _categoryRepository.GetAllAsync()
            };
        }

        public async Task<ResultModel<Property>> GetPropertiesAsync()
        {
            return new ResultModel<Property>
            {
                IsSuccess = true,
                Items = await _propertyRepository.GetAllAsync()
            };
        }


        public async Task<ResultModel<Drink>> SearchByNameAsync(string search)
        {
            //use the IQueryable
            var drinks = _drinkRepository.GetAll();
            var searchResult = await drinks
                .Where(d => d.Name.ToUpper()
                .Contains(search.ToUpper()))
                .ToListAsync();
            return new ResultModel<Drink>
            {
                IsSuccess = true,
                Items = searchResult
            };
        }

        public async Task<ResultModel<Drink>> UpdateAsync(int id, string name, int categoryId, int alcoholPercentage, IEnumerable<int> propertyIds)
        {
            //check if category
            if(!await _categoryRepository.GetAll().AnyAsync(c =>
            c.Id == categoryId))
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Unkown Category." }
                };
            }
            //check if properties
            var properties = _propertyRepository
                .GetAll().Count(p => propertyIds.Contains(p.Id));
            if(properties != propertyIds.Count())
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = false,
                    Errors = new 
                    List<string> { "Unknown property" }
                };
            }
            //get the entity
            var toUpdate = await _drinkRepository.GetByIdAsync(id);
            //if name changes => check if name already exists
            if (!toUpdate.Name.ToUpper().Equals(name.ToUpper()) 
                &&
                await _drinkRepository.GetAll().AnyAsync(d => d.Name.ToUpper().Equals(name.ToUpper())))
            {
                return new ResultModel<Drink>
                {
                        IsSuccess = false,
                        Errors = new List<string> { "Name exists!" }
                };
            }
            //update the entity
            toUpdate.Name = name;
            toUpdate.AlcoholPercentage = alcoholPercentage;
            toUpdate.CategoryId = categoryId;
            toUpdate.Properties = await _propertyRepository
                .GetAll().Where(p => propertyIds.Contains(p.Id)).ToListAsync();
            if(await _drinkRepository.UpdateAsync(toUpdate))
            {
                return new ResultModel<Drink>
                {
                    IsSuccess = true,
                };
            }
            return new ResultModel<Drink>
            {
                IsSuccess = false,
                Errors = new List<string> { "Something went wrong, please try again later..." }
            };
        }
    }
}
