using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pri.Drinks.Core.Entities;
using Pri.Drinks.Core.Interfaces.Repositories;
using Pri.Drinks.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Infrastructure.Repositories
{
    public class DrinkRepository : BaseRepository<Drink>, IDrinkRepository
    {
        public DrinkRepository(ApplicationDbContext applicationDbContext,
            ILogger<BaseRepository<Drink>> logger) 
            : base(applicationDbContext, logger)
        {
        }

        public override IQueryable<Drink> GetAll()
        {
            return _table
                .Include(d => d.Properties)
                .Include(d => d.Category)
                .AsQueryable();
        }

        public async override Task<IEnumerable<Drink>> GetAllAsync()
        {
            return await _table
                .Include(d => d.Properties)
                .Include(d => d.Category).ToListAsync();
        }

        public Task<IEnumerable<Drink>> GetByCategory(int id)
        {
            //TODO
            throw new NotImplementedException();
        }

        public async override Task<Drink> GetByIdAsync(int id)
        {
            return await _table
                .Include(d => d.Properties)
                .Include(d => d.Category).FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
