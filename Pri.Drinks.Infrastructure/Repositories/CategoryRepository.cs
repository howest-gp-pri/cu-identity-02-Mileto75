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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext, ILogger<BaseRepository<Category>> logger) : base(applicationDbContext, logger)
        {
        }
    }
}
