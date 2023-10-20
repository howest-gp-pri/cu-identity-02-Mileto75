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
    public abstract class BaseRepository<T> 
        : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<BaseRepository<T>> _logger;
        protected readonly DbSet<T> _table;

        protected BaseRepository(ApplicationDbContext applicationDbContext, ILogger<BaseRepository<T>> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _table = _applicationDbContext.Set<T>();
        }

        public async Task<bool> CreateAsync(T toCreate)
        {
            toCreate.Created = DateTime.Now;
            _table.Add(toCreate);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(T toDelete)
        {
            _table.Remove(toDelete);
            return await SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _table.AsQueryable();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _table.FirstOrDefaultAsync
               (d => d.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogCritical(dbUpdateException.InnerException.Message);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T toUpdate)
        {
            var entity = await GetByIdAsync(toUpdate.Id);
            entity = toUpdate;
            entity.Updated = DateTime.Now;
            return await SaveChangesAsync();
        }
    }
}
